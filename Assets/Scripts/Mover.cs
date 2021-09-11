using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour, IControllable
{
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
            ghost.ChangeColor(Color.yellow);
        }
    }

    public void HandleSelected()
    {
        Ghost ghost = GetComponent<Ghost>();
        
        if (ghost)
        {
            ghost.ChangeColor(Color.red);
        }
    }

    public void SetTargetLocation(Vector2 newTarget)
    {
        targetPosition = newTarget;
    }

    private void Move()
    {
        Vector2 targetDirection = (targetPosition - (Vector2)transform.position).normalized;
        transform.position += (Vector3)(targetDirection * moveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        targetPosition = transform.position;
    }
}
