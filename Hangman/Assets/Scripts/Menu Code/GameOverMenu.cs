using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    public Text title;

    public GameObject soloObject;
    public Text scoreOutput;
    public Text highscoreOutput;
    public Text wordOutput;
    

    public GameObject multiplayerObject;
    public Text p1scoreOutput;
    public Text p2scoreOutput;
    public Text p1WordOutput;
    public Text p2WordOutput;


    private bool multiplayer;

    public void Awake()
    {
        multiplayer = (1 == PlayerPrefs.GetInt("Multiplayer"));
        

        if (multiplayer)
        {
            int p1score = PlayerPrefs.GetInt("Player One Score");
            int p2score = PlayerPrefs.GetInt("Player Two Score");
            string p1name = (PlayerPrefs.GetString("Player One Name"));
            string p2name = (PlayerPrefs.GetString("Player Two Name"));



            multiplayerObject.SetActive(true);
            if(p1score >= p2score)
            {
                title.text = p1name+" Won!";
            }
            else
            {
                title.text = p2name + " Won!";
            }

            p1scoreOutput.text = (PlayerPrefs.GetString("Player One Name")) + " : " + p1score;
            p2scoreOutput.text = (PlayerPrefs.GetString("Player Two Name")) + " : " + p2score;
            p1WordOutput.text = "Word was : " + PlayerPrefs.GetString("P One Word").ToLower();
            p2WordOutput.text = "Word was : " + PlayerPrefs.GetString("P Two Word").ToLower();
        }

        else
        {
            soloObject.SetActive(true);
            title.text = "GAME OVER!";
            int highscore = PlayerPrefs.GetInt("Highscore");
            highscoreOutput.text = "Highscore : " + highscore;
            int score = PlayerPrefs.GetInt("Score");
            scoreOutput.text = "Score : " + score;
            wordOutput.text = "Word was : " + PlayerPrefs.GetString("Word").ToLower();
        }
        
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }
}
