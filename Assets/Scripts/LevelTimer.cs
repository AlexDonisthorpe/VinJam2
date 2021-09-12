using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LevelTimer : MonoBehaviour
{
    [SerializeField] private float levelDurationInSeconds = 60f;
    
    private bool _timer = false;
    [SerializeField] private float levelTimer = 60f;
    
    // Start is called before the first frame update
    void Start()
    {
        StartTimer();
    }

    private void StartTimer()
    {
        levelTimer = levelDurationInSeconds;
        _timer = true;
    }

    // Update is called once per frame
    private void Update()
    {
        if (_timer)
        {
            levelTimer -= Time.deltaTime;
            if(levelTimer <= levelDurationInSeconds) Debug.Log("Level Complete");
        }
        
    }
}
