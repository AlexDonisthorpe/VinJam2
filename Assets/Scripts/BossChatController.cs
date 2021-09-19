using System.Collections;
using TMPro;
using UnityEngine;

public class BossChatController : MonoBehaviour
{
    private static readonly int StartMessage = Animator.StringToHash("ShowMessage");

    private Animator _animator;
    private static readonly int PortraitIn = Animator.StringToHash("PortraitIn");
    private bool _bportraitIn = false;
    
    private static readonly int TextIn = Animator.StringToHash("TextIn");
    private bool _btextIn = false;

    [SerializeField] private AudioClip BossDialogueSFX;
    private AudioController _audioController;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _audioController = FindObjectOfType<AudioController>();
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
        if(_btextIn && BossDialogueSFX != null) _audioController.PlaySFX(ref BossDialogueSFX);

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
        
        // So that it only plays if the textbox is active
        if (_btextIn && BossDialogueSFX != null) _audioController.PlaySFX(ref BossDialogueSFX);
    }
}
