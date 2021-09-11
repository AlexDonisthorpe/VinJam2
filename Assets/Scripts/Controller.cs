using System;
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
            
            // Check for objects where you click
            Vector3 screenPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos = new Vector2(screenPos.x, screenPos.y);
            
            RaycastHit2D hitData = Physics2D.Raycast(mousePos, Vector2.zero, 500);
            if (hitData)
            {
                // Deselect the current selected Object
                if (selectedObject)
                {
                    selectedObject.GetComponent<IControllable>().HandleDeselect();
                }
                
                // Select the new object
                selectedObject = hitData.transform.gameObject;
                selectedObject.GetComponent<IControllable>().HandleSelected();
            }
        }

        if (selectedObject != null && Input.GetMouseButtonDown(1))
        {
            selectedObject.GetComponent<IControllable>().HandleRightClick();
        }
    }


}
