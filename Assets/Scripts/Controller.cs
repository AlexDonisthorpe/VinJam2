using UnityEngine;

public class Controller : MonoBehaviour
{
    private GameObject selectedObject;
    private bool isPaused = false;
    private bool hasStarted = false;

    void Update()
    {
        if(!hasStarted) return;
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                FindObjectOfType<PauseMenu>().Continue();
                isPaused = false;
            }
            else if (selectedObject == null)
            {
                FindObjectOfType<PauseMenu>().Pause();
                isPaused = true;
            }
            else
            {
                Deselect(selectedObject);
            }
        }

        if (isPaused) return;
        
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
            if (!selectedObject) return;
            
            if (selectedObject.GetComponent<Ghost>())
            {
                RaycastHit2D hitData = CheckForControllers();
                
                if (hitData)
                {
                    House targetHouse = hitData.transform.gameObject.GetComponent<House>();
                    selectedObject.GetComponent<Ghost>().SetTargetHouse(targetHouse);
                }
                else
                {
                    selectedObject.GetComponent<Ghost>().SetTargetHouse(null);
                }

            }

            selectedObject.GetComponent<IControllable>().HandleRightClick();
        }


    }

    public void Deselect(GameObject objectCallingDeselect)
    {
        if (!selectedObject) return;
        if (objectCallingDeselect != selectedObject) return;
        
        selectedObject.GetComponent<IControllable>().HandleDeselect();
        selectedObject = null;
    }

    private RaycastHit2D CheckForControllers()
    {
        // Check for objects where you click
        Vector3 screenPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos = new Vector2(screenPos.x, screenPos.y);

        return Physics2D.Raycast(mousePos, Vector2.zero, 500);
    }

    public void StartGame()
    {
        hasStarted = true;
    }
}
