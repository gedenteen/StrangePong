//using System;
using System.Collections;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Ball : MonoBehaviour
{
    /// Public fields
    [HideInInspector] public Rigidbody2D rb2d;
    [HideInInspector] public BallAudio ballAudio;
    [HideInInspector] public ParticleSystem collisionParticle;
    public float maxInitialAngle = 0.67f;
    public float moveSpeed = 1f;
    public float rotationDuration = 0.3f; /// Duration in seconds
    public int rotationCount = 5; /// Number of rotations
    public float maxStartY = 4f;
    public float speedMultiplier = 1.1f;

    /// Private fields
    private float startX = 0f;

    private void Start()
    {
        rb2d = GetComponentInChildren<Rigidbody2D>();
        collisionParticle = GetComponentInChildren<ParticleSystem>();

        InitialPush();

        GameManager.instance.onReset += ResetBall; /// subscribe to event
    }

    private void OnDestroy()
    {
        GameManager.instance.onReset -= ResetBall;
    }

    private void InitialPush()
    {
        Vector2 dir = UnityEngine.Random.value < 0.5f ? Vector2.left : Vector2.right;
        dir.y = UnityEngine.Random.Range(-maxInitialAngle, maxInitialAngle);
        rb2d.velocity = dir * moveSpeed;

        EmitParticle(16);
    }

    IEnumerator ResetBallCoroutine()
    {
        /// play sound
        ballAudio.PlayResetBallSound();

        /// stop the ball
        rb2d.velocity = Vector2.zero;

        /// position
        float posY = UnityEngine.Random.Range(-maxStartY, maxStartY);
        Vector2 pos = new Vector2(startX, posY);
        transform.position = pos;

        /// rotation
        for (int i = 0; i < rotationCount; i++)
        {
            float elapsedTime = 0f;
            Quaternion startRotation = transform.rotation;
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, 90f);

            while (elapsedTime < rotationDuration)
            {
                transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime / rotationDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.rotation = new Quaternion();
        }

        /// push
        InitialPush();
    }

    public void ResetBall()
    {
        StartCoroutine(ResetBallCoroutine());
    }

    /// Method for collisons with ScoreZone
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ScoreZone scoreZone = collision.GetComponent<ScoreZone>();
        if (scoreZone != null)
        {
            GameManager.instance.OnScoreZoneReached(scoreZone.id);
            //StartCoroutine(ResetBallCoroutine());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Paddle paddle = collision.collider.GetComponent<Paddle>();
        if (paddle != null)
        {
            ballAudio.PlayPaddleSound();
            rb2d.velocity *= speedMultiplier;
            EmitParticle(4);
        }

        Wall wall = collision.collider.GetComponent<Wall>();
        if (wall != null)
        {
            ballAudio.PlayWallSound();
            EmitParticle(4);
        }
    }

    private void EmitParticle(int amount)
    {
        collisionParticle.Emit(amount);
    }
}
