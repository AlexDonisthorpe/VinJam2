using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class LevelTimer : MonoBehaviour
{
    [SerializeField] private float levelDurationInSeconds = 300f;
    [SerializeField] private float levelTimer = 0f;
    private bool _timer = false;
    [SerializeField] private float _maxSectionDuration;
    [SerializeField] private float _sectionTimer = 0;
    [SerializeField] private AudioClip levelMusic;

    private Slider _slider;
    private int section = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        _maxSectionDuration = levelDurationInSeconds / 5;
        
        _slider = GetComponentInChildren<Slider>();
        _slider.maxValue = levelDurationInSeconds;
    }

    public void StartTimer()
    {
        _sectionTimer = 0;
        levelTimer = 0;
        _timer = true;
        FindObjectOfType<Controller>().StartGame();
        FindObjectOfType<HouseController>().StartHousing();
        GetComponent<PlayableDirector>().Play();
        FindObjectOfType<AudioController>().PlayMusic(ref levelMusic);
    }

    // Update is called once per frame
    private void Update()
    {
        _slider.value = levelTimer;
        
        if (_timer)
        {
            levelTimer += Time.deltaTime;
            _sectionTimer += Time.deltaTime;
            
            if (_sectionTimer >= 60)
            {
                _sectionTimer = 0;
                FindObjectOfType<HouseController>().UpdateTotalHouses();
                FindObjectOfType<CameraScaler>().ScaleOut();
                AstarPath.active.Scan();
                
                section++;
                if (section > 1)
                {
                    FindObjectOfType<PartyGhostSpawner>().IncreasePartyGhosts();
                }
            }
        }
        
        
        
    }
}
