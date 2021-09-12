using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BossChatController : MonoBehaviour
{
    private static readonly int StartMessage = Animator.StringToHash("StartMessage");

    private void Start()
    {
        GetComponent<Animator>().SetTrigger(StartMessage);
    }

    public void ShowMessage(string message)
    {
        GetComponentInChildren<TextMeshProUGUI>().text = message;
        GetComponent<Animator>().SetTrigger(StartMessage);
    }
}
