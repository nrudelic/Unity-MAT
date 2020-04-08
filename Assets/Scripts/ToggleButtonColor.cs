using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ToggleButtonColor : MonoBehaviour
{
    public GameObject button;
    private Toggle theButton;
    private ColorBlock theColor;
    public GameObject[] toggles;

    private void Start()
    {
        toggles = GameObject.FindGameObjectsWithTag("NumberOfTasks");
        ButtonTransitionColors();

    }

    public void ButtonTransitionColors()
    {
        foreach(GameObject toggleGameObj in toggles)
        {
            Toggle toggle = toggleGameObj.GetComponent<Toggle>();
            ColorBlock color = toggle.colors;
            if (toggle.isOn)
            {
                int numberOfTasksInt = FindValue(toggleGameObj.name);
                color.normalColor = Color.blue;
                color.pressedColor = Color.blue;
                color.highlightedColor = Color.blue;
                PlayerPrefs.SetInt("NumberOfTasks", numberOfTasksInt);
                print(PlayerPrefs.GetInt("NumberOfTasks"));
            } else
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
            case "Option5":
                return 5;
            case "Option10":
                return 10;
            case "Option15":
                return 15;
            case "Option20":
                return 20;
            case "OptionInf":
                return 0;
        }

        return 0;
    }
}
