using TMPro;
using UnityEngine;

public class PlayerChatController : MonoBehaviour
{
    private static readonly int StartMessage = Animator.StringToHash("StartMessage");

    public void ShowMessage(string message)
    {
        GetComponentInChildren<TextMeshProUGUI>().text = message;
        GetComponent<Animator>().SetTrigger(StartMessage);
    }
}
