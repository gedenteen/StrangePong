using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Public Fields
    public static GameManager instance;
    public Action onReset;

    // Links to be set in the Inspector
    public int scorePlayer1, scorePlayer2;
    public ScoreText scoreTextLeft, scoreTextRight;
    public TextMeshProUGUI winText;

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
        HighlightScore(id);
    }

    private void UpdateScores()
    {
        scoreTextLeft.SetScore(scorePlayer1);
        scoreTextRight.SetScore(scorePlayer2);

        if (scorePlayer1 == 0)
        {
            OnGameEnds(1);
        }
        else if (scorePlayer2 == 0)
        {
            OnGameEnds(2);
        }
        else
        {
            onReset?.Invoke(); /// reset ball
        }
    }

    private void OnGameEnds(int winnerId)
    {
        Debug.Log($"OnGameEnds: winner {winnerId}");
        MainMenu.instance.ChangeTextOfMainLabel($"Player {winnerId} wins!");
        MainMenu.instance.gameObject.SetActive(true);
    }

    public void HighlightScore(int id)
    {
        if (id == 1)
        {
            scoreTextLeft.Highlight();
        }
        else if (id == 2)
        {
            scoreTextRight.Highlight();
        }
    }
}
