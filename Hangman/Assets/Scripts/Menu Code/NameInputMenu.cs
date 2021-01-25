using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NameInputMenu : MonoBehaviour
{
    public void Start()
    {
        PlayerPrefs.SetString("Player One Name", "Player 1");
        PlayerPrefs.SetString("Player Two Name", "Player 2");
        PlayerPrefs.SetInt("Player One Score", 0);
        PlayerPrefs.SetInt("Player Two Score", 0);
        PlayerPrefs.SetInt("Player One Death", 0);
        PlayerPrefs.SetInt("Player One Death", 0);
    }

    public void PlayerOneName(string name)
    {
        PlayerPrefs.SetString("Player One Name",name);
    }

    public void PlayerTwoName(string name)
    {
        PlayerPrefs.SetString("Player Two Name", name);
    }

    public void Play()
    {
        PlayerPrefs.SetInt("Multiplayer", 1);
        PlayerPrefs.SetInt("Turn", 0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
    }
}
