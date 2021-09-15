using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    private Controller controllerRef;
    private static readonly int Selected = Animator.StringToHash("Selected");

    // Start is called before the first frame update
    void Start()
    {
        controllerRef = FindObjectOfType<Controller>();
    }

    public void EnterHouse()
    {
        GetComponent<Mover>().Stop();
        GetComponent<SpriteRenderer>().enabled = false;
        controllerRef.Deselect(gameObject);
    }

    public void LeaveHouse(Vector2 spawnPos)
    {
        transform.position = (Vector3)spawnPos;
        GetComponent<SpriteRenderer>().enabled = true;
    }

    public void ChangeColor(Color color)
    {
        GetComponent<SpriteRenderer>().color = color;
    }

    public void Select()
    {
        GetComponent<Animator>().SetBool(Selected, true);
    }

    public void Unselect()
    {
        GetComponent<Animator>().SetBool(Selected, false);
    }
}
