using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public void Awake()
    {
        if (PlayerPrefs.HasKey("Lives").Equals(null))
        {
            //Game Options presets
            PlayerPrefs.SetInt("Lives", 5);
            PlayerPrefs.SetInt("Max Word Length", 15);
            PlayerPrefs.SetInt("Min Word Length", 1);
            PlayerPrefs.SetInt("Show Lives", 1);
            PlayerPrefs.SetInt("Highscore",0);

            PlayerPrefs.SetFloat("Volume", -16.5f);
            PlayerPrefs.SetInt("Multiplayer", 0);
            PlayerPrefs.SetString("P1 Word", "N/A");
            PlayerPrefs.SetString("P2 Word", "N/A");
            PlayerPrefs.SetInt("Player Death", 0);
        }
    }

    public void Start()
    {
        float volume = PlayerPrefs.GetFloat("Volume");
        audioMixer.SetFloat("GameMusic", volume);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
