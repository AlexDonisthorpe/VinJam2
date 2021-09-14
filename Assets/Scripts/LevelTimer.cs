using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LevelTimer : MonoBehaviour
{
    [SerializeField] private float levelDurationInSeconds = 300f;
    [SerializeField] private float levelTimer = 300f;
    private bool _timer = false;
    [SerializeField] private float _maxSectionDuration;
    [SerializeField] private float _sectionTimer;

    private Slider _slider;
    
    // Start is called before the first frame update
    void Start()
    {
        _maxSectionDuration = levelDurationInSeconds / 5;
        _sectionTimer = _maxSectionDuration;
        
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
            if (levelTimer <= 0)
            {
                // End of Level
                
                Debug.Log("Level Complete");
                return;
            }

            _sectionTimer -= Time.deltaTime;
            if (_sectionTimer <= 0)
            {
                _sectionTimer = _maxSectionDuration;
                FindObjectOfType<HouseController>().UpdateTotalHouses();
                FindObjectOfType<CameraScaler>().ScaleOut();
            }
        }
        
        
        
    }
}
