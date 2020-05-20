using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LogoScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt("Addition") == 0)
        {
            PlayerPrefs.SetInt("Addition", 1);
        }
        if (PlayerPrefs.GetInt("Substraction") == 0)
        {
            PlayerPrefs.SetInt("Substraction", 1);
        }
        if (PlayerPrefs.GetInt("Multiplication") == 0)
        {
            PlayerPrefs.SetInt("Multiplication", 1);
        }
        if (PlayerPrefs.GetInt("Division") == 0)
        {
            PlayerPrefs.SetInt("Division", 1);
        }
        if (PlayerPrefs.GetString("typeOfAnswer").Equals(""))
        {
            PlayerPrefs.SetString("typeOfAnswer", "Numbers");
        }
        if (PlayerPrefs.GetInt("NumberOfTasks") == 0)
        {
            PlayerPrefs.SetInt("NumberOfTasks", 5);
        }
        if (PlayerPrefs.GetInt("NumberOfAnswers") == 0)
        {
            PlayerPrefs.SetInt("NumberOfAnswers", 2);
        }
        if (PlayerPrefs.GetInt("toNumber") == 0)
        {
            PlayerPrefs.SetInt("toNumber", 5);
        }
        Invoke("loadMain", 2);
    }

    public void loadMain()
    {
        SceneManager.LoadScene(1);
    }
}
