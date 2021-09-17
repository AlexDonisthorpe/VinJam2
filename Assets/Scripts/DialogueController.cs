using System;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    [SerializeField] private Checkin[] _checkins;
    [TextArea] [SerializeField] private String[] bossMessages;
    [TextArea] [SerializeField] private String[] playerMessages;
    
    private static int checkinCounter = 0;
    private static int bossMessageCounter = 0;
    private static int playerMessageCounter = 0;

    private BossChatController _bossChatController;
    private PlayerChatController _playerChatController;

    private void Start()
    {
        _bossChatController = FindObjectOfType<BossChatController>();
        _playerChatController = FindObjectOfType<PlayerChatController>();
    }

    public void BossCheckin()
    {
        String messageToSend;
        
        if (FindObjectOfType<LevelController>().totalHauntings < _checkins[checkinCounter].successHousesHaunted)
        {
            messageToSend = _checkins[checkinCounter].failText;
            FindObjectOfType<LevelController>().UpdateChances();
        }
        else
        {
            messageToSend = _checkins[checkinCounter].successText;
        }
        
        _bossChatController.ShowMessage(messageToSend);
        if (checkinCounter == 4)
        {
            Invoke(nameof(EndGame), 5f);
        }
        checkinCounter++;
    }

    public void ToggleBossPortrait()
    {
        _bossChatController.TogglePortrait();
    }

    public void ToggleBossChat()
    {
        _bossChatController.ToggleTextBar();
    }
    
    public void BossChat()
    {
        _bossChatController.ShowMessage(bossMessages[bossMessageCounter]);
        bossMessageCounter++;
    }

    public void TogglePlayerPortrait()
    {
        _playerChatController.TogglePortrait();
    }

    public void TogglePlayerChat()
    {
        _playerChatController.ToggleTextBar();
    }
    
    public void PlayerChat()
    {
        FindObjectOfType<PlayerChatController>().ShowMessage(playerMessages[playerMessageCounter]);
        playerMessageCounter++;
    }

    private void EndGame()
    {
        FindObjectOfType<LevelController>().EndGame(true);
    }
}
