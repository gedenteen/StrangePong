using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public int id;
    public float moveSpeed = 2f;
    public float aiDeadZoneY = 0.8f;
    //public float aiDeadZoneX = 1.2f;
    public float timeForDoNothing = 0.1f;

    private Vector3 startPosition;
    private int direction = 0; //direction to move
    private float moveSpeedMultiplier = 1f;
    private float timerToDoNothing = 0;
    private bool thisPlayerIsAi = false; //set it in Awake()

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        thisPlayerIsAi = Settings.instance.IsThisPlayerAi(id);
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
        switch (Settings.instance.difficultLevel)
        {
            case 0:
                aiDeadZoneY = 2.2f;
                break;
            case 1:
                aiDeadZoneY = 1.6f;
                break;
            case 2:
                aiDeadZoneY = 1f;
                break;
            default:
                //Debug.LogError("Unexpcted difficult level");
                break;
        }

        if (thisPlayerIsAi)
        {
            MoveAi();
        }
        else
        {
            float movement = ProcessInput();
            Move(movement);
        }
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

    public void MoveAi()
    {
        //transform.position = new Vector2(startPosition.x, ballPos.y); // Unbeatable AI code

        if (timerToDoNothing > 0)
        {
            timerToDoNothing = Mathf.Clamp(timerToDoNothing - Time.deltaTime, 0f, 2f);
            Move(direction);
            return;
        }

        Vector2 ballPos = GameManager.instance.ball.transform.position;
        Vector2 ballVelocity = GameManager.instance.ball.rb2d.velocity;

        if ((id == 1 && ballVelocity.x > 0) || (id == 2 && ballVelocity.x < 0))
        {
            // Hold position
            direction = 0;
            Move(direction);
            return;
        }

        float xDifference = ballPos.x - transform.position.x; //x-distance between ball and this paddle
        float timeToReach = xDifference / ballVelocity.x; // time ro reach this paddle
        float yDifference = ballPos.y + ballVelocity.y * timeToReach - transform.position.y; //y-distance taking into account the movement of the ball
        if (Mathf.Abs(yDifference) > aiDeadZoneY)
        {
            direction = yDifference > 0 ? 1 : -1;
            timerToDoNothing = timeForDoNothing;
        }
        Move(direction);
    }

    private void Move(float movement)
    {
        Vector2 velo = rb2d.velocity;
        velo.y = movement * moveSpeedMultiplier * moveSpeed;
        rb2d.velocity = velo;
    }
}
