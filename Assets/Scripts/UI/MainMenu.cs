using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static MainMenu instance;

    public TextMeshProUGUI mainLabel;

    private void Awake()
    {
        instance = this;
    }

    private void OnLevelWasLoaded(int level)
    {
        ChangeTextOfMainLabel("Pause");
    }

    public void ChangeTextOfMainLabel(string text)
    {
        mainLabel.text = text;
    }

    public void Singleplayer()
    {
        SceneManager.LoadScene("Level1");
    }
}
