//using System;
using System.Collections;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Ball : MonoBehaviour
{
    // Public fields
    public Rigidbody2D rb2d;
    public float maxInitialAngle = 0.67f;
    public float moveSpeed = 1f;
    public float rotationDuration = 0.3f; // Duration in seconds
    public int rotationCount = 5; // Number of rotations
    public float maxStartY = 4f;

    // Private fields
    private float startX = 0f;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        InitialPush();
    }

    private void InitialPush()
    {
        Vector2 dir = UnityEngine.Random.value < 0.5f ? Vector2.left : Vector2.right;
        dir.y = UnityEngine.Random.Range(-maxInitialAngle, maxInitialAngle);
        rb2d.velocity = dir * moveSpeed;
    }

    IEnumerator ResetBall()
    {
        rb2d.velocity = Vector2.zero;

        // position
        float posY = UnityEngine.Random.Range(-maxStartY, maxStartY);
        Vector2 pos = new Vector2(startX, posY);
        transform.position = pos;

        // rotation
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

        InitialPush();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ScoreZone scoreZone = collision.GetComponent<ScoreZone>();
        if (scoreZone != null)
        {
            GameManager.instance.OnScoreZoneReached(scoreZone.id);
            StartCoroutine(ResetBall());
        }
    }
}
