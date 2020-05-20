using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Summary : MonoBehaviour
{
    public TextMeshProUGUI right;
    public TextMeshProUGUI wrong;
    public TextMeshProUGUI time;

    void Start()
    {
        int numberOfRight = PlayerPrefs.GetInt("NumberOfRightAnswers");        
        int numberOfWrong = PlayerPrefs.GetInt("NumberOfWrongAnswers");
        String totalTime = PlayerPrefs.GetString("TotalTime");
        right.text = "" + numberOfRight;
        wrong.text = "" + numberOfWrong;
        time.text = "" + totalTime;
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("NextScene"));
    }
}
