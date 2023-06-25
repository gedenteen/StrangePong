using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAudio : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip resetBallSound;
    public AudioClip wallSound;
    public AudioClip paddleSound;

    private void Awake()
    {
        audioSource.volume = AudioManager.instance.audioSourceSounds.volume;
        AudioManager.instance.onSoundsVolumeChange += ChangeVolumeSounds;
    }

    private void OnDestroy()
    {
        AudioManager.instance.onSoundsVolumeChange -= ChangeVolumeSounds;
    }

    public void PlayResetBallSound()
    {
        audioSource.PlayOneShot(resetBallSound);
    }
    public void PlayWallSound()
    {
        audioSource.PlayOneShot(wallSound);
    }
    public void PlayPaddleSound()
    {
        audioSource.PlayOneShot(paddleSound);
    }

    public void ChangeVolumeSounds(float volume)
    {
        audioSource.volume = volume;
    }
}
