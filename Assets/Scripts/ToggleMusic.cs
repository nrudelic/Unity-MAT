using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ToggleMusic : MonoBehaviour
{
    public GameObject button;
    private Toggle theButton;
    private ColorBlock theColor;
    public GameObject[] toggles;

    private void Start()
    {
        toggles = GameObject.FindGameObjectsWithTag("Music");
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
                color.normalColor = Color.blue;
                color.pressedColor = Color.blue;
                color.highlightedColor = Color.blue;

                if (toggleGameObj.name.Equals("MusicYes"))
                {
                    PlayerPrefs.SetInt("Music", 1);
                } else
                {
                    PlayerPrefs.SetInt("Music", 0);
                }
                print(PlayerPrefs.GetInt("Music"));
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

}
