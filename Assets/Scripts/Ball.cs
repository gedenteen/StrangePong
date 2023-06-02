//using System;
using System.Collections;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public float maxInitialAngle = 0.67f;
    public float moveSpeed = 1f;

    public float rotationDuration = 1f; // Duration in seconds
    public int rotationCount = 3; // Number of rotations

    private float startX = 0f;
    public float maxStartY = 4f;

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

        /*float elapsedTime = 0f;
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, 180f); // Replace with desired final rotation

        while (elapsedTime < timeForRotation)
        {
            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, elapsedTime / timeForRotation);
            elapsedTime += Time.deltaTime;
            yield return null;
        }*/
        for (int i = 0; i < rotationCount; i++)
        {
            float elapsedTime = 0f;
            Quaternion startRotation = transform.rotation;
            Debug.Log("transform.rotation=" + transform.rotation);
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, (i%2+1) * 180f);

            while (elapsedTime < rotationDuration)
            {
                transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime / rotationDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }

        InitialPush();
    }

    private void ResetBallPart2()
    {
        // direction
        InitialPush();

        // position
        float posY = UnityEngine.Random.Range(-maxStartY, maxStartY);
        Vector2 pos = new Vector2(startX, posY);
        transform.position = pos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ScoreZone scoreZone = collision.GetComponent<ScoreZone>();
        if (scoreZone != null)
        {
            StartCoroutine(ResetBall());
        }
    }
}
