using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerToggle : MonoBehaviour
{
    public GameObject button;
    private Toggle theButton;
    private ColorBlock theColor;
    public GameObject[] toggles;

    private void Start()
    {
        toggles = GameObject.FindGameObjectsWithTag("NumberOfAnswers");
        ButtonTransitionColors();

    }

    public void ButtonTransitionColors()
    {
        foreach (GameObject toggleGameObj in toggles)
        {
            Toggle toggle = toggleGameObj.GetComponent<Toggle>();
            ColorBlock color = toggle.colors;
            if (toggle.isOn)
            {
                int numberOfAnsInt = FindValue(toggleGameObj.name);
                color.normalColor = Color.blue;
                color.pressedColor = Color.blue;
                color.highlightedColor = Color.blue;
                PlayerPrefs.SetInt("NumberOfAns", numberOfAnsInt);
                print(PlayerPrefs.GetInt("NumberOfAns"));
            }
            else
            {
                color.normalColor = Color.white;
                color.pressedColor = Color.white;
                color.highlightedColor = Color.white;
            }
            toggle.colors = color;
        }
    }
    private int FindValue(string name)
    {
        switch (name)
        {
            case "Option2":
                return 2;
            case "Option3":
                return 3;
            case "Option4":
                return 4;
            case "Option5":
                return 5;
            case "Option6":
                return 6;
        }

        return 0;
    }
}
