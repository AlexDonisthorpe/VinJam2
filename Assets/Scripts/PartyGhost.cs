using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PartyGhost : MonoBehaviour
{
    [SerializeField] private List<Transform> _destinations;
    [SerializeField] private float targetSwapRange = .5f;

    private Mover _mover;
    private Vector2 _targetDestination;
    private void Start()
    {
        _targetDestination = transform.position;
        _mover = GetComponent<Mover>();
    }

    private void GetNewDestination()
    {
        int destIndex = Random.Range(0, _destinations.Count - 1);
        _targetDestination = _destinations[destIndex].position;
        _mover.SetTargetLocation(_targetDestination);
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, _targetDestination) < targetSwapRange)
        {
            GetNewDestination();
        }
    }
}
