using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioSource _sfxSource;
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _pauseSource;

    private bool isPaused = false;
    
    public void TogglePauseMusic()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            _musicSource.Pause();
            _pauseSource.Play();
        }
        else
        {
            _pauseSource.Pause();
            _musicSource.Play();
        }
    }

    public void PlayMusic(ref AudioClip music)
    {
        _musicSource.Stop();
        _musicSource.clip = music;
        _musicSource.Play();
    }

    public void PlaySFX(ref AudioClip sfx)
    {
        _sfxSource.PlayOneShot(sfx);
    }
}
