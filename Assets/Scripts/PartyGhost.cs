using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PartyGhost : MonoBehaviour
{
    [SerializeField] private float targetSwapRange = .5f;

    private Mover _mover;
    private Vector2 _targetDestination;
    private bool partying = false;
    private HouseController _houseController;
    private void Start()
    {
        _targetDestination = transform.position;
        _houseController = FindObjectOfType<HouseController>();
        _mover = GetComponent<Mover>();
    }

    private void GetNewDestination()
    {
        _targetDestination = _houseController.GetRandomHouse().transform.position;
        _mover.SetTargetLocation(_targetDestination);
    }

    private void Update()
    {
        if (partying) return;
        if (Vector2.Distance((Vector2)transform.position, (Vector2)_targetDestination) < targetSwapRange)
        {
            Debug.Log("Distance");
            GetNewDestination();
        }
    }

    public void Party()
    {
        StartCoroutine(StartParty());
    }

    IEnumerator StartParty()
    {
        partying = true;
        _mover.SetTargetLocation(transform.position);
        yield return new WaitForSeconds(10f);
        partying = false;
        GetNewDestination();
    }
}
