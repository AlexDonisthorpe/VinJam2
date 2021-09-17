using System;
using System.Collections;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BossChatController : MonoBehaviour
{
    private static readonly int StartMessage = Animator.StringToHash("ShowMessage");

    private Animator _animator;
    private static readonly int PortraitIn = Animator.StringToHash("PortraitIn");
    private bool _bportraitIn = false;
    
    private static readonly int TextIn = Animator.StringToHash("TextIn");
    private bool _btextIn = false;
    

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void TogglePortrait()
    {
        _bportraitIn = !_bportraitIn;
        _animator.SetBool(PortraitIn, _bportraitIn);
    }
    
    public void ToggleTextBar()
    {
        _btextIn = !_btextIn;
        _animator.SetBool(TextIn, _btextIn);
    }

    public void ShowMessage(string message)
    {
        StartCoroutine(ChangeTextDelayed(message));
    }

    IEnumerator ChangeTextDelayed(string message)
    {
        _animator.SetTrigger(StartMessage);
        yield return new WaitForSeconds(1f);
        GetComponentInChildren<TextMeshProUGUI>().text = message;
    }
}
