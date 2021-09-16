using System;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    [SerializeField] private Checkin[] _checkins;
    private static int checkinCounter = 0;

    
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
        checkinCounter++;
    }
}
