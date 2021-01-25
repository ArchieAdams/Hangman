using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOptionsMenu : MonoBehaviour
{
    public Text livesOutput;
    public Text maxWordLenOutput;
    public Text minWordLenOutput;

    public Slider livesSlider;
    public Slider maxWordLenSlider;
    public Slider minWordLenSlider;


    public void Awake()
    {
        livesSlider.value = PlayerPrefs.GetInt("Lives");
        maxWordLenSlider.value = PlayerPrefs.GetInt("Max Word Length");
        minWordLenSlider.value = PlayerPrefs.GetInt("Min Word Length");
        livesOutput.text = "LIVES : " + PlayerPrefs.GetInt("Lives");
        maxWordLenOutput.text = "MAX WORD LENGTH : " + PlayerPrefs.GetInt("Max Word Length");
        minWordLenOutput.text = "MIN WORD LENGTH :" + PlayerPrefs.GetInt("Min Word Length");
    }

    public void lives(float livesFloat)
    {
        int lives = (int) livesFloat;
        livesOutput.text = "LIVES : " + lives;
        PlayerPrefs.SetInt("Lives", lives);
    }

    public void maxWordLen(float maxWordLenFloat)
    {
        int maxWordLen = (int)maxWordLenFloat;
        maxWordLenOutput.text = "MAX WORD LENGTH : " + maxWordLen;
        minWordLenSlider.maxValue = maxWordLen;
        PlayerPrefs.SetInt("Max Word Length", maxWordLen);
    }

    public void minWordLen(float minWordLenFloat)
    {
        int minWordLen = (int)minWordLenFloat;
        minWordLenOutput.text = "MIN WORD LENGTH : " + minWordLen;
        maxWordLenSlider.minValue = minWordLen;
        PlayerPrefs.SetInt("Min Word Length", minWordLen);
    }
    public void showLives(bool showLives)
    {
        if(showLives)
            PlayerPrefs.SetInt("Show Lives", 1);
        else
            PlayerPrefs.SetInt("Show Lives", 0);
    }
}
