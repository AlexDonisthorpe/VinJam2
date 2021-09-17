using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerChatController : MonoBehaviour
{
    private static readonly int StartMessage = Animator.StringToHash("ShowMessage");

    private Animator _animator;
    private static readonly int PortraitIn = Animator.StringToHash("PortraitIn");
    private bool _pportraitIn = false;
    
    private static readonly int TextIn = Animator.StringToHash("TextIn");
    private bool _ptextIn = false;
    

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void TogglePortrait()
    {
        _pportraitIn = !_pportraitIn;
        _animator.SetBool(PortraitIn, _pportraitIn);
    }
    
    public void ToggleTextBar()
    {
        _ptextIn = !_ptextIn;
        _animator.SetBool(TextIn, _ptextIn);
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
