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
            
            RaycastHit2D hitData = CheckForControllers();

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

        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit2D hitData = CheckForControllers();
           
            if (hitData)
            {
                IControllable controllable = hitData.transform.gameObject.GetComponent<IControllable>();
                if (controllable != null) controllable.HandleRightClick();
            }
            else
            {
                selectedObject.GetComponent<IControllable>().HandleRightClick();
            }
            
        }
    }

    public void Deselect()
    {
        selectedObject = null;
    }

    private RaycastHit2D CheckForControllers()
    {
        // Check for objects where you click
        Vector3 screenPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos = new Vector2(screenPos.x, screenPos.y);

        return Physics2D.Raycast(mousePos, Vector2.zero, 500);
    }

}
