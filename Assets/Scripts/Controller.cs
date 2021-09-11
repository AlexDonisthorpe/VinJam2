using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private GameObject selectedObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 screenPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos = new Vector2(screenPos.x, screenPos.y);

            RaycastHit2D hitData = Physics2D.Raycast(mousePos, Vector2.zero, 500);
            if (hitData)
            {
                if (selectedObject)
                {
                    selectedObject.GetComponent<SpriteRenderer>().color = Color.yellow;
                }
                selectedObject = hitData.transform.gameObject;
                selectedObject.GetComponent<SpriteRenderer>().color = Color.red;
                Debug.Log("Selected: " + selectedObject.name);
            }
        }

        if (selectedObject != null && Input.GetMouseButtonDown(1))
        {
            Mover objectMover = selectedObject.GetComponent<Mover>();
            if (!objectMover) return;
            
            Debug.Log("Moving");
            objectMover.SetTargetLocation(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }
}
