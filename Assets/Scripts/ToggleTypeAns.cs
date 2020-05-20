using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToggleTypeAns : MonoBehaviour
{
    public GameObject button;
    private Toggle theButton;
    private ColorBlock theColor;
    public GameObject[] toggles;
    public Text min, max;
    public Slider slider;
    public Toggle addition, substraction, multiplication, division;

    private void Start()
    {
        toggles = GameObject.FindGameObjectsWithTag("TypeAns");
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
                setMinMaxText(toggle);
                ruleOperations(toggle);
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

    private void ruleOperations(Toggle toggle)
    {
        switch (toggle.gameObject.name)
        {
            case "NumbersOption":
                multiplication.interactable = true;
                division.interactable = true;
                break;
            case "SymbolsOption":
                multiplication.interactable = false;
                division.interactable = false;
                multiplication.isOn = false;
                division.isOn = false;
                if(addition.isOn == false && substraction.isOn == false)
                {
                    addition.isOn = true;
                    substraction.isOn = true;
                }
                break;
            case "BlocksOption":
                multiplication.interactable = false;
                division.interactable = false;
                multiplication.isOn = false;
                division.isOn = false;
                if (addition.isOn == false && substraction.isOn == false)
                {
                    addition.isOn = true;
                    substraction.isOn = true;
                }
                break;
        }
    }

    private void setMinMaxText(Toggle toggle)
    {
        switch (toggle.gameObject.name)
        {
            case "NumbersOption":
                slider.maxValue = 99;
                min.text = "5";
                max.text = "99";
                break;
            case "SymbolsOption":
                slider.maxValue = 9;
                min.text = "5";
                max.text = "9";
                break;
            case "BlocksOption":
                slider.maxValue = 30;
                min.text = "5";
                max.text = "30";
                break;
        }
    }
}
