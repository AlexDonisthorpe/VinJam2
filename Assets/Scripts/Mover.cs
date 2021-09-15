using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Mover : MonoBehaviour, IControllable
{
    [SerializeField] float moveSpeed = 200f;
    [SerializeField] private float nextWaypointDistance = 3f;

    private Path path;
    private int currentWaypoint = 0;
    private Vector2 targetPosition;

    private Seeker _seeker;
    private Rigidbody2D _rigidbody2D;

    private void Start()
    {
        _seeker = GetComponent<Seeker>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        targetPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (path == null) return;
        if (currentWaypoint >= path.vectorPath.Count) return;
        
        Move();
    }

    public void HandleRightClick()
    {
        Vector3 screenPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos = new Vector2(screenPos.x, screenPos.y);
        SetTargetLocation(mousePos);
    }

    public void HandleDeselect()
    {
        Ghost ghost = GetComponent<Ghost>();
        
        if (ghost)
        {
            ghost.Unselect();
        }
    }

    public void HandleSelected()
    {
        Ghost ghost = GetComponent<Ghost>();
        
        if (ghost)
        {
            ghost.Select();
        }
    }

    public void SetTargetLocation(Vector2 newTarget)
    {
        targetPosition = newTarget;
        _seeker.StartPath(_rigidbody2D.position, targetPosition, OnPathComplete);
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    private void Move()
    {
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - _rigidbody2D.position).normalized;
        Vector2 force = direction * moveSpeed * Time.deltaTime;
        _rigidbody2D.AddForce(force);

        float distance = Vector2.Distance(_rigidbody2D.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance) currentWaypoint++;
        
        GetComponent<SpriteRenderer>().flipX = !(direction.x > 0);
    }

    public void Stop()
    {
        path = null;
    }
}
