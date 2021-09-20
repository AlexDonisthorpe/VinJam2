using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlay : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    
    public void ReturnToMain()
    {
        mainMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}
