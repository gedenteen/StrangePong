using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    // Public fields
    public static Settings instance = null;
    public PlayMode playMode;
    public int difficultLevel = 0;

    public enum PlayMode
    {
        PlayerVsPlayer,
        PlayerVsAi,
        AiVsAi
    }

    private void Awake()
    {
        if (instance)
        {
            Debug.LogError("There is another Settings object (it should be singleton)");
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        SceneManager.LoadScene("AiVsAi");
        SetPlayMode(PlayMode.AiVsAi);
    }

    public void SetPlayModePlayerVsPlayer()
    {
        playMode = PlayMode.PlayerVsPlayer;
    }

    public void SetPlayModePlayerVsAi()
    {
        playMode = PlayMode.PlayerVsAi;
    }

    public void SetPlayMode(PlayMode playMode)
    {
        this.playMode = playMode;
    }

    /*public bool IsPlayer2Ai()
    {
        return playMode == PlayMode.PlayerVsAi || playMode == PlayMode.AiVsAi;
    }

    public bool IsPlayer1Ai()
    {
        return playMode == PlayMode.AiVsAi;
    }*/

    public bool IsThisPlayerAi(int playerId)
    {
        if (playerId == 1)
        {
            return playMode == PlayMode.AiVsAi;
        }
        else if (playerId == 2)
        {
            return playMode == PlayMode.PlayerVsAi || playMode == PlayMode.AiVsAi;
        }
        else
        {
            Debug.LogError($"Unexpected player id {playerId}");
            return false;
        }
    }
}
