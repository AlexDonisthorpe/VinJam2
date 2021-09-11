using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour, IControllable
{
    [SerializeField] private bool isActive = false;
    [SerializeField] private int maxGhosts = 1;
    [SerializeField] private int currentGhostCounter = 0;
    [SerializeField] private Transform ghostSpawn;

    private List<Ghost> storedGhosts;

    private void Start()
    {
        storedGhosts = new List<Ghost>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (currentGhostCounter == maxGhosts) return;

        Ghost ghost = other.gameObject.GetComponent<Ghost>();
        if (!ghost) return;
        
        Debug.Log("Entering House");
        ghost.EnterHouse();
        
        Debug.Log("Storing Ghost");
        storedGhosts.Add(ghost);
        currentGhostCounter++;

        if(currentGhostCounter == maxGhosts) GetComponent<SpriteRenderer>().color = Color.blue;
    }

    public void HandleRightClick()
    {
        if (currentGhostCounter == 0) return;
        
        storedGhosts[storedGhosts.Count-1].LeaveHouse(ghostSpawn.position);
        storedGhosts.RemoveAt(storedGhosts.Count - 1);
        currentGhostCounter--;

        if (currentGhostCounter == 0) GetComponent<SpriteRenderer>().color = Color.cyan;
    }

    public void HandleDeselect()
    {
        return;
    }

    public void HandleSelected()
    {
        return;
    }
}
