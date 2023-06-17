using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public float id;
    public float moveSpeed = 2f;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float movement = ProcessInput();
        Move(movement);
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

        Debug.Log("movement = " + movement);

        return movement;
    }

    private void Move(float movement)
    {
        Vector2 velo = rb2d.velocity;
        velo.y = movement * moveSpeed;
        Debug.Log("velo=" + velo);
        rb2d.velocity = velo;
    }
}
