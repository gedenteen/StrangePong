using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public int id;
    public float moveSpeed = 2f;

    private Vector3 startPosition;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        startPosition = transform.position;
        GameManager.instance.onReset += ResetPosition;
    }

    private void OnDestroy()
    {
        GameManager.instance.onReset -= ResetPosition;
    }

    private void Update()
    {
        float movement = ProcessInput();
        Move(movement);
    }

    private void ResetPosition()
    {
        transform.position = startPosition;
    }

    private float ProcessInput()
    {
        float movement = 0f;

        switch (id)
        {
            case 1:
                movement = Input.GetAxis("MovePlayer1");
                break;
            case 2:
                movement = Input.GetAxis("MovePlayer2");
                break;
            default:
                Debug.LogError($"Unexpected id={id} in ProcessInput for Paddle");
                break;
        }

        return movement;
    }

    private void Move(float movement)
    {
        Vector2 velo = rb2d.velocity;
        velo.y = movement * moveSpeed;
        rb2d.velocity = velo;
    }
}
