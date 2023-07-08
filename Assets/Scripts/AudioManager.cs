using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Refs to Audio Sources")]
    public AudioSource audioSourceSounds;
    public AudioSource audioSourceMusic;

    [Header("Refs to music")]
    public AudioClip mainMenuMusic;

    [Header("Refs to sounds")]
    public AudioClip scoreSound;
    public AudioClip winSound;
    public AudioClip looseSound;

    public Action<float> onSoundsVolumeChange;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnLevelWasLoaded(int level)
    {
        if (level > 1)
        {
            audioSourceMusic.Stop();
        }
    }

    #region PlaySounds
    public void PlayScoreSound()
    {
        audioSourceSounds.PlayOneShot(scoreSound);
    }
    public void PlayWinSound()
    {
        audioSourceSounds.PlayOneShot(winSound);
    }
    public void PlayLooseSound()
    {
        audioSourceSounds.PlayOneShot(looseSound);
    }
    #endregion

    public void ChangeVolumeMusic(float volume)
    {
        audioSourceMusic.volume = volume;
    }
    public void ChangeVolumeSounds(float volume)
    {
        audioSourceSounds.volume = volume;
        onSoundsVolumeChange?.Invoke(volume);
    }

}
