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
        
        FindObjectOfType<BossChatController>().ShowMessage(messageToSend);
        if (checkinCounter == 4)
        {
            Invoke(nameof(EndGame), 5f);
        }
        checkinCounter++;
    }

    public void BossChat()
    {
        FindObjectOfType<BossChatController>().ShowMessage(bossMessages[bossMessageCounter]);
        bossMessageCounter++;
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
