using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    // Public fields
    public static Settings instance = null;
    public PlayMode playMode;
    public int difficultLevel = 0;

    public enum PlayMode
    {
        PlayerVsPlayer,
        PlayerVsAi
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

    public bool IsPlayerVsAi()
    {
        return playMode == PlayMode.PlayerVsAi;
    }
}
