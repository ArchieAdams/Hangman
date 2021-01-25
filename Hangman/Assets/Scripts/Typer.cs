using System.Collections;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Typer : MonoBehaviour
{
    public GameObject playerNameObject;
    public GameObject highscoreObject;

    public Text wordOutput;
    public Text liveOutput;
    public Text scoreOutput;
    public Text highscoreOutput;
    public Text guessedLettersOutput;
    public Text playerNameOutput;

    public TextAsset textFile;

    public ArrayList wordsList = new ArrayList();
    public ArrayList guessedLettersList = new ArrayList();


    private string outputWord = string.Empty;
    private string remainingWord = string.Empty;
    private string currentWord = string.Empty;

    private bool isGameOver = false;

    private int score = 0;
    private int highscore;

    private int lives;

    private int minLength;
    private int maxLength;

    private bool showLives;

    private bool multiplayer;

    private ArrayList playerNames = new ArrayList();
    private int turn;

    private void Start()
    {
        multiplayer = (1 == PlayerPrefs.GetInt("Multiplayer"));
        if (0 == PlayerPrefs.GetInt("Turn"))
            PlayerPrefs.SetInt("Turn", 1);
        else
            PlayerPrefs.SetInt("Turn", 0);

        lives = PlayerPrefs.GetInt("Lives");
        maxLength = PlayerPrefs.GetInt("Max Word Length");
        minLength = PlayerPrefs.GetInt("Min Word Length");
        showLives = (1 == PlayerPrefs.GetInt("Show Lives"));

        highscore = PlayerPrefs.GetInt("Highscore");
        highscoreOutput.text = "Highscore : " + highscore;

        playerNames.Add(PlayerPrefs.GetString("Player One Name"));
        playerNames.Add(PlayerPrefs.GetString("Player Two Name"));
        turn = PlayerPrefs.GetInt("Turn");

        SetLives();
        if (multiplayer)
        {
            highscoreObject.SetActive(false);
            SetPlayer();
            if (turn == 0)
                score = PlayerPrefs.GetInt("Player One Score");
            else
                score = PlayerPrefs.GetInt("Player Two Score");

            scoreOutput.text = "Score : " + score;
        }
        SetCurrentWord();
    }

    private void SetCurrentWord()
    {
        FileReader(); // Reads word bank
        SetRemainingWord(currentWord);
    }

    private void SetRemainingWord(string currentWord)
    {
        remainingWord = currentWord;
        Debug.Log(remainingWord);
        wordOutput.text = outputWord; // Displays word
    }

    private void Update()
    {
        if(!isGameOver)
            CheckInput();  //Happens every frame
    }

    private void CheckInput()
    {
        if (Input.anyKeyDown)
        {
            string keysPressed = Input.inputString;

            if (keysPressed.Length == 1)
                EnterLetter(keysPressed);
        }
    }

    private void EnterLetter(string typedLetter)
    {
        char typedChar = (char)typedLetter.ToLower().ToCharArray().GetValue(0);
        if (typedChar >= 97 && typedChar <= 122 && !(guessedLettersList.Contains(typedLetter)) && !(outputWord.Contains(typedLetter)) ||
            (typedChar >= 65 && typedChar <= 90 && !(guessedLettersList.Contains(typedLetter)) && !(outputWord.Contains(typedLetter))))
        {
            if (IsCorrectLetter(typedLetter))
            {
                RemoveLetter(typedLetter);

                if (IsWordComplete())
                    GameWin();
            }
            else
            {
                
                DisplayGuessedLetters(typedLetter);
                lives -= 1;
                if (lives == 0)
                    GameOver();
                SetLives();
            }
        }
    }

    private bool IsCorrectLetter(string letter)
    {
        return remainingWord.Contains(letter.ToLower()); 
    }

    private void RemoveLetter(string letter)
    {

        char letterChar = (char)(letter.ToCharArray().GetValue(0));

        for (int i = 0; i < remainingWord.Length; i++)
        {
            if (remainingWord.ToCharArray().GetValue(i).Equals(letterChar))
            {
                outputWord = outputWord.Remove(i, 1).Insert(i, letter);
            }
        }

        string removedLetterWord = remainingWord.Replace(letterChar, (char)0x20);

        SetRemainingWord(removedLetterWord);
    }

    private bool IsWordComplete()
    {
        foreach (char letter in remainingWord)
        {
            if (letter != (char)0x20)
                return false;
        }
        return true;

    }

    private void SetLives()
    {
        if(showLives)
            liveOutput.text = "Lives : "+ lives;
        else
            liveOutput.text = "Lives : ?";
    }

    private void GameOver()
    {
        FindObjectOfType<AudioManager>().Play("Scream");
        if (multiplayer)
        {
            if (turn == 0)
            {
                PlayerPrefs.SetInt("Player One Death", 1);
                PlayerPrefs.SetString("P One Word", currentWord);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
            }
            else
            {
                PlayerPrefs.SetString("P Two Word", currentWord);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }

        else
        {
            PlayerPrefs.SetInt("Score", score);
            PlayerPrefs.SetString("Word", currentWord);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        
    }
    
    private void GameWin()
    {
        score++;
        if (multiplayer)
        {

            if (1 == PlayerPrefs.GetInt("Player One Death"))
                PlayerPrefs.SetInt("Turn", 0);


            if (turn == 0)
                PlayerPrefs.SetInt("Player One Score", score);
            else
                PlayerPrefs.SetInt("Player Two Score", score);

            scoreOutput.text = "Score : " + score;

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        }

        else
        {
            FindObjectOfType<AudioManager>().Play("Win");
            
            scoreOutput.text = "Score : " + score;
            if (score > highscore)
            {
                highscore = score;
                highscoreOutput.text = "Highscore : " + highscore;
                PlayerPrefs.SetInt("Highscore", highscore);
            }
            ResetAfterWin();
            SetCurrentWord();
        }
    }

    private void FileReader()
    {
        if (multiplayer)
            currentWord = PlayerPrefs.GetString("Word").ToLower();

        else { 
            
            string[] lines = File.ReadAllLines(Path.Combine(Application.streamingAssetsPath, "Words.txt"));


            string word = string.Empty;

            while (true)
            {
                word = lines[Random.Range(0, lines.Length + 1)].ToString();
                if ((word.Length >= minLength) && (word.Length <= maxLength)) {
                    currentWord = word;
                    break;
                }
            }
        }

        outputWord = string.Empty;
        for (int i = 0; i < currentWord.Length; i++)
        {
            outputWord += "-"; 
        }
    }

    private void DisplayGuessedLetters(string letter)
    {
        guessedLettersList.Add(letter);
        string guessedLetters = string.Empty;
        foreach (string guessedLetter in guessedLettersList)
        {
            guessedLetters += guessedLetter.ToUpper() + "\n";
        }
        guessedLettersOutput.text = "Guessed\nLetters\n" + guessedLetters;
    }

    public void ResetAfterWin()
    {
        lives = PlayerPrefs.GetInt("Lives");
        guessedLettersList.Clear();
        guessedLettersOutput.text = "Guessed\nLetters\n";
    }

    public void SetPlayer()
    {
        playerNameObject.SetActive(true);
        playerNameOutput.text = (string) playerNames[turn]+"'s turn";
    }
}
