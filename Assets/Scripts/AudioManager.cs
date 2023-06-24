using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource audioSourceSounds;

    /*public AudioClip resetBallSound;
    public AudioClip wallSound;
    public AudioClip paddleSound;*/
    public AudioClip scoreSound;
    public AudioClip winSound;
    public AudioClip looseSound;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

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
}
