using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LevelTimer : MonoBehaviour
{
    [SerializeField] private float levelDurationInSeconds = 60f;
    
    [SerializeField] private float levelTimer = 60f;
    private bool _timer = false;

    private Slider _slider;
    
    // Start is called before the first frame update
    void Start()
    {
        _slider = GetComponentInChildren<Slider>();
        _slider.maxValue = levelDurationInSeconds;
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
        _slider.value = levelTimer;
        
        if (_timer)
        {
            levelTimer -= Time.deltaTime;
            if(levelTimer <= 0) Debug.Log("Level Complete");
        }
        
    }
}
