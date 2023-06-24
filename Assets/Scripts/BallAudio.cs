using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAudio : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip resetBallSound;
    public AudioClip wallSound;
    public AudioClip paddleSound;

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
}
