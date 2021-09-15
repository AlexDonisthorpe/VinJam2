using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour, IControllable
{
    [SerializeField] private static float stopRadius = 0.1f;
    [SerializeField] float moveSpeed = 5f;

    private Vector2 targetPosition;

    private void Start()
    {
        targetPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
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
    }

    private void Move()
    {
        if(Vector2.Distance(transform.position, targetPosition) < stopRadius) return;
        
        Vector2 targetDirection = (targetPosition - (Vector2)transform.position).normalized;
        transform.Translate(targetDirection * moveSpeed * Time.deltaTime);

        GetComponent<SpriteRenderer>().flipX = !(targetDirection.x > 0);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        targetPosition = transform.position;
    }
}
