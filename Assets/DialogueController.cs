using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    [SerializeField] private Checkin[] _checkins;
    private static int checkinCounter = 0;

    
    public void BossCheckin()
    {
        string messageToSend =
            (FindObjectOfType<LevelController>().totalHauntings < _checkins[checkinCounter].successHousesHaunted)
                ? _checkins[checkinCounter].failText
                : _checkins[checkinCounter].successText;
        
        FindObjectOfType<BossChatController>().ShowMessage(messageToSend);
        checkinCounter++;
    }
}
