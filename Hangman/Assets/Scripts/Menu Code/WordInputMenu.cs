using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WordInputMenu : MonoBehaviour
{
    public Text playerNameOutput;
    public GameObject submitObject;
    public InputField wordInput;

    public ArrayList playerNames = new ArrayList();
    public int turn;
    void Start()
    {        
        playerNames.Add(PlayerPrefs.GetString("Player One Name"));
        playerNames.Add(PlayerPrefs.GetString("Player Two Name"));
        turn = PlayerPrefs.GetInt("Turn");
        playerNameOutput.text =(string) playerNames[turn]+" :";
    }

    public void Word(string word)
    {
        char typedChar = (char)word.ToLower().ToCharArray().GetValue(word.Length - 1);
        if (typedChar >= 97 && typedChar <= 122 ||
            (typedChar >= 65 && typedChar <= 90))
        {
            submitObject.SetActive(true);
            PlayerPrefs.SetString("Word", word);
        }
        else
            wordInput.text = word.Remove(word.Length - 1,1);
    }

    public void Submit()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }
}
