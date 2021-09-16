using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Boss Check-in", menuName = "ScriptableObjects/Checkin", order = 1)]

public class Checkin : ScriptableObject
{
    [TextArea]
    public string successText = "success";

    [TextArea]
    public string failText = "fail";

    public float successHousesHaunted = 1f;
}
