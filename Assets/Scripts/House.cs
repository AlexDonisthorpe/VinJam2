using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;

public class House : MonoBehaviour, IControllable
{
    [SerializeField] private GameObject GhostPrefab;
    
    
    [SerializeField] private float hauntingTimer = 10f;
    [SerializeField] private bool isEnabled = false;
    [SerializeField] private int maxGhosts = 1;
    [SerializeField] private int currentGhostCounter = 0;
    [SerializeField] private Transform ghostSpawn;
    [SerializeField] private GameObject infoUICanvas;

    [SerializeField] private static float tickRate = 3.0f;
    private float _currentCounter = 0;

    private int pointsPerTick = 1;
    
    private List<Ghost> _storedGhosts;
    private LevelController _levelController;

    private void Start()
    {
        GetComponentInChildren<SpriteRenderer>().color = Color.blue;
        _storedGhosts = new List<Ghost>();
        _levelController = FindObjectOfType<LevelController>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!isEnabled || currentGhostCounter == maxGhosts) return;

        Ghost ghost = other.gameObject.GetComponent<Ghost>();
        if (!ghost) return;
        
        Debug.Log("Entering House");
        ghost.EnterHouse();
        
        Debug.Log("Storing Ghost");
        _storedGhosts.Add(ghost);
        currentGhostCounter++;
        UpdateUI();
    }

    public void HandleRightClick()
    {
        if (currentGhostCounter == 0) return;
        
        _storedGhosts[_storedGhosts.Count-1].LeaveHouse(ghostSpawn.position);
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
        if (currentGhostCounter == 0 || !isEnabled) return;
        
        _currentCounter += Time.deltaTime;

        if (!(_currentCounter >= tickRate)) return;
        
        _currentCounter = 0;
        _levelController.updateScore(pointsPerTick * currentGhostCounter);
    }

    public bool GetEnabled()
    {
        return isEnabled;
    }

    public void SetEnabled()
    {
        StartCoroutine(Haunting());
    }

    private IEnumerator Haunting()
    {
        isEnabled = true;
        GetComponentInChildren<SpriteRenderer>().color = Color.cyan;
        yield return new WaitForSeconds(hauntingTimer);
        isEnabled = false;
        foreach (Ghost ghost in _storedGhosts)
        {
            ghost.LeaveHouse(ghostSpawn.position);
            --currentGhostCounter;
        }
        _storedGhosts.Clear();
        GetComponentInParent<HouseController>().DecreaseActiveHouses();
        GetComponentInChildren<SpriteRenderer>().color = Color.blue;

        Instantiate(GhostPrefab, ghostSpawn.position, Quaternion.identity, transform.parent);
        UpdateUI();
    }
}
