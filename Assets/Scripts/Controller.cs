using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
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
            if(hitData) Debug.Log(hitData.transform.gameObject.name);
        }
    }
}
