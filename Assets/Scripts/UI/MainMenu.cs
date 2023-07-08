using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public static MainMenu instance;

    public TextMeshProUGUI mainLabel;
    public Slider sliderMusicVolume;
    public TextMeshProUGUI textMusicVolume;
    public Slider sliderSoundsVolume;
    public TextMeshProUGUI textSoundsVolume;

    private void Awake()
    {
        instance = this;
        ChangeVolumeMusic(sliderMusicVolume.value);
        ChangeVolumeSounds(sliderSoundsVolume.value);
    }

    private void OnLevelWasLoaded(int level)
    {
        if (level > 1)
        {
            ChangeTextOfMainLabel("Pause");
        }
    }

    public void ChangeTextOfMainLabel(string text)
    {
        mainLabel.text = text;
    }

    public void Singleplayer()
    {
        SceneManager.LoadScene("Level1");
    }

    public void ChangeVolumeMusic(float value)
    {
        AudioManager.instance.ChangeVolumeMusic(value);
        textMusicVolume.text = $"{Mathf.RoundToInt(value * 100)}%";
    }
    public void ChangeVolumeSounds(float value)
    {
        AudioManager.instance.ChangeVolumeSounds(value);
        textSoundsVolume.text = $"{Mathf.RoundToInt(value * 100)}%";
    }
}
