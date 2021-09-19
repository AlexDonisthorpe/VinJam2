using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class House : MonoBehaviour, IControllable
{
    [FormerlySerializedAs("GhostPrefab")] [SerializeField] private GameObject ghostPrefab;
    
    // Timer parameters for tracking the duration of the haunting
    [SerializeField] private float hauntingTimer = 10f;
    [SerializeField] private float _currentTimer = 0;

    [SerializeField] private float partyTimer = 10f;
    
    // Timer parameters for tracking how long a ghost has been in the house
    // to track contribution
    private float maxGhostTimer = 0f;
    [SerializeField] private float currentGhostTimer = 0f;

    // AKA isCurrentlyHaunting
    [SerializeField] private bool isEnabled = false;
    
    // Tracking how how ghosts are allowed inside the house
    // / are in the house currently
    [SerializeField] private int maxGhosts = 1;
    [SerializeField] private int currentGhostCounter = 0;
    
    // Location for where ghosts should spawn & offsets for variation
    [SerializeField] private Transform ghostSpawn;
    [SerializeField] private float minOffset = 0f;
    [SerializeField] private float maxOffset = 3f;

    // UI / Sprites
    [SerializeField] private GameObject infoUICanvas;
    [SerializeField] private Sprite activeSprite;
    [SerializeField] private Sprite inactiveSprite;
    [SerializeField] private GameObject selectedUI;
    
    // List of ghosts in the house
    private List<Ghost> _storedGhosts;
    
    // Cached References
    private LevelController _levelController;
    private SpriteRenderer _childSpriteRenderer;
    [SerializeField] private Transform ghostParent;

    private bool party = false;
    
    private void OnEnable()
    {
        _childSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _storedGhosts = new List<Ghost>();
        _levelController = FindObjectOfType<LevelController>();
        maxGhostTimer = hauntingTimer / 2;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (party) return;
        if (!isEnabled || currentGhostCounter == maxGhosts) return;

        Ghost ghost = other.gameObject.GetComponent<Ghost>();
        if (!ghost) return;

        if (ghost.GetTargetHouse() == this)
        {
            ghost.EnterHouse();
        
            _storedGhosts.Add(ghost);
            currentGhostCounter++;
            UpdateUI();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (party || !isEnabled || currentGhostCounter == 0) return;
        if (other.GetComponent<PartyGhost>() != null)
        {
            other.GetComponent<PartyGhost>().Party();
            StartParty();
        }
    }

    public void StartParty()
    {
        if (isEnabled && currentGhostCounter > 0)
        {
            StartCoroutine(Party());
        }
    }

    IEnumerator Party()
    {
        party = true;
        KickGhostsOut();
        yield return new WaitForSeconds(partyTimer);
        party = false;
    }

    public void HandleRightClick()
    {
        if (currentGhostCounter == 0) return;
        
        _storedGhosts[_storedGhosts.Count-1].LeaveHouse(OffSetHousePosition());
        _storedGhosts.RemoveAt(_storedGhosts.Count - 1);
        currentGhostCounter--;
        UpdateUI();
    }

    public void HandleDeselect()
    {
        selectedUI.SetActive(false);
    }

    public void HandleSelected()
    {
        selectedUI.SetActive(true);
    }

    private void UpdateUI()
    {
        if (!infoUICanvas) return;

        String details = $"{currentGhostCounter} / {maxGhosts}";
        infoUICanvas.GetComponentInChildren<TextMeshProUGUI>().text = details;
    }

    public String GetGhostsString()
    {
        return $"{currentGhostCounter} / {maxGhosts}";
    }

    private void Update()
    {
        if (!isEnabled) return;
        
        float counterMultiplier = Mathf.Clamp(currentGhostCounter, 1f, 3f);
        _currentTimer += Time.deltaTime * counterMultiplier;
        
        if (currentGhostCounter > 0) currentGhostTimer += Time.deltaTime * counterMultiplier;
        
        if (_currentTimer >= hauntingTimer)
        {
            isEnabled = false;
            _currentTimer = 0;
            
            GetComponentInParent<HouseController>().DecreaseActiveHouses();
            _childSpriteRenderer.sprite = inactiveSprite;
            
            if (currentGhostTimer < maxGhostTimer)
            {
                currentGhostTimer = 0;

                KickGhostsOut();
                return;
            }
            
            currentGhostTimer = 0;
            
            if (currentGhostCounter == 0) return;
            
            Instantiate(ghostPrefab, ghostSpawn.position, Quaternion.identity, ghostParent);
            _levelController.UpdateTotalHauntings();

            foreach (Ghost ghost in _storedGhosts)
            {
                ghost.LeaveHouse(OffSetHousePosition());
                --currentGhostCounter;
            }

            _storedGhosts.Clear();
            UpdateUI();
        }
        
    }

    private void KickGhostsOut()
    {
        foreach (Ghost ghost in _storedGhosts)
        {
            
            ghost.LeaveHouse(OffSetHousePosition());
            if (party) ghost.StartParty(partyTimer); 

            --currentGhostCounter;
        }

        _storedGhosts.Clear();
    }

    public bool GetEnabled()
    {
        return isEnabled;
    }

    public void SetEnabled()
    {
        isEnabled = true;
        _childSpriteRenderer.sprite = activeSprite;
    }

    private Vector2 OffSetHousePosition()
    {
        float xOffset = Random.Range(minOffset, maxOffset);
        float yOffset = Random.Range(minOffset, maxOffset);

        var position = ghostSpawn.position;
        return new Vector2(position.x + xOffset, position.y + yOffset);
    }
}
