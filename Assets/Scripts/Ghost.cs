using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    private Controller controllerRef;
    
    // Start is called before the first frame update
    void Start()
    {
        controllerRef = FindObjectOfType<Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnterHouse()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        controllerRef.Deselect(gameObject);
    }

    public void LeaveHouse(Vector2 spawnPos)
    {
        transform.position = (Vector3)spawnPos;
        GetComponent<Mover>().SetTargetLocation(spawnPos);
        GetComponent<SpriteRenderer>().enabled = true;
    }

    public void ChangeColor(Color color)
    {
        GetComponent<SpriteRenderer>().color = color;
    }
}
