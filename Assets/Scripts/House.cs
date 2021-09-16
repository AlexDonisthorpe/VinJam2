using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class House : MonoBehaviour, IControllable
{
    [FormerlySerializedAs("GhostPrefab")] [SerializeField] private GameObject ghostPrefab;
    
    
    [SerializeField] private float hauntingTimer = 10f;
    [SerializeField] private bool isEnabled = false;
    [SerializeField] private int maxGhosts = 1;
    [SerializeField] private int currentGhostCounter = 0;
    [SerializeField] private Transform ghostSpawn;
    [SerializeField] private GameObject infoUICanvas;
    [SerializeField] private Sprite activeSprite;
    [SerializeField] private Sprite inactiveSprite;
    [SerializeField] private float _currentTimer = 0;
    private List<Ghost> _storedGhosts;
    private LevelController _levelController;
    private SpriteRenderer _childSpriteRenderer;

    private float maxGhostTimer = 0f;
    [SerializeField] private float currentGhostTimer = 0f;

    [SerializeField] private float minOffset = 0f;
    [SerializeField] private float maxOffset = 3f;

    private void OnEnable()
    {
        _childSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _storedGhosts = new List<Ghost>();
        _levelController = FindObjectOfType<LevelController>();
        maxGhostTimer = hauntingTimer / 2;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
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
        UpdateUI();
        infoUICanvas.SetActive(false);
    }

    public void HandleSelected()
    {
        UpdateUI();
        infoUICanvas.SetActive(true);
    }

    private void UpdateUI()
    {
        if (!infoUICanvas) return;

        String details = $"{currentGhostCounter} / {maxGhosts}";
        infoUICanvas.GetComponentInChildren<TextMeshProUGUI>().text = details;
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
                
                foreach (Ghost ghost in _storedGhosts)
                {
                    ghost.LeaveHouse(OffSetHousePosition());
                    --currentGhostCounter;
                }

                _storedGhosts.Clear();
                
                return;
            }
            
            currentGhostTimer = 0;
            
            if (currentGhostCounter == 0) return;
            
            Instantiate(ghostPrefab, ghostSpawn.position, Quaternion.identity, transform.parent);
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
