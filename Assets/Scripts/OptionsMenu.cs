using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private AudioMixer mixer;

    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider musicSlider;

    public void ReturnToMain()
    {
        mainMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    public void SetMasterLevel(float sliderValue)
    {
        mixer.SetFloat("MasterVol", Mathf.Log10(sliderValue) * 20);
    }
    
    public void SetMusicLevel(float sliderValue)
    {
        mixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
    }
    
    public void SetSFXLevel(float sliderValue)
    {
        mixer.SetFloat("SFXVol", Mathf.Log10(sliderValue) * 20);
    }

    private void OnEnable()
    {
        float value;
        mixer.GetFloat("MasterVol", out value);
        masterSlider.value = Mathf.Pow(10.0f, value / 20.0f);

        mixer.GetFloat("MusicVol", out value);
        musicSlider.value = Mathf.Pow(10.0f, value / 20.0f);

        mixer.GetFloat("SFXVol", out value);
        sfxSlider.value = Mathf.Pow(10.0f, value / 20.0f);
    }
}
