using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    [SerializeField] private bool isActive = false;
    [SerializeField] private int maxGhosts = 1;
    [SerializeField] private int currentGhostCounter = 0;

    private List<Ghost> storedGhosts;


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (currentGhostCounter == maxGhosts) return;

        Ghost ghost = other.gameObject.GetComponent<Ghost>();
        if (!ghost) return;
        
        ghost.EnterHouse();
        storedGhosts.Add(ghost);
        currentGhostCounter++;

        if(currentGhostCounter == maxGhosts) GetComponent<SpriteRenderer>().color = Color.blue;
    }
}
