using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyGhostSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnLocations;
    [SerializeField] private GameObject partyGhostPrefab;
    
    private int maxPartyGhosts = 0;
    private int currentPartyGhosts = 0;

    void Update()
    {
        if (maxPartyGhosts > currentPartyGhosts)
        {
            SpawnPartyGhost();
        }
    }

    private void SpawnPartyGhost()
    {
        currentPartyGhosts++;

        int spawnLocationIndex = Random.Range(0, spawnLocations.Length - 1);

        Instantiate(partyGhostPrefab, spawnLocations[spawnLocationIndex].position, Quaternion.identity, gameObject.transform);
    }

    public void IncreasePartyGhosts()
    {
        maxPartyGhosts++;
    }
}
