using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Public Fields
    public static GameManager instance;

    // Links to be set in the Inspector
    public int scorePlayer1, scorePlayer2;
    public ScoreText scoreTextLeft, scoreTextRight;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("There is 2 (at least) GameManagers (should be 1)");
            return;
        }

        UpdateScores();
    }

    public void OnScoreZoneReached(int id)
    {
        if (id == 1)
        {
            scorePlayer1--;
        }
        else if (id == 2)
        {
            scorePlayer2--;
        }
        else
        {
            Debug.LogError($"Unxpected id={id} of ScoreZone");
        }

        UpdateScores();
    }

    private void UpdateScores()
    {
        scoreTextLeft.SetScore(scorePlayer1);
        scoreTextRight.SetScore(scorePlayer2);
    }
}