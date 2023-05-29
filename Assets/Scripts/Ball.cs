using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public float maxInitialAngle = 0.67f;
    public float moveSpeed = 1f;

    private float startX = 0f;
    public float maxStartY = 4f;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        InitialPush();
    }

    private void InitialPush()
    {
        Vector2 dir = Random.value < 0.5f ? Vector2.left : Vector2.right;
        dir.y = Random.Range(-maxInitialAngle, maxInitialAngle);
        rb2d.velocity = dir * moveSpeed;
    }

    private void ResetBall()
    {
        // direction
        InitialPush();

        // position
        float posY = Random.Range(-maxStartY, maxStartY);
        Vector2 pos = new Vector2(startX, posY);
        transform.position = pos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ScoreZone scoreZone = collision.GetComponent<ScoreZone>();
        if (scoreZone != null)
        {
            ResetBall();
        }
    }
}
