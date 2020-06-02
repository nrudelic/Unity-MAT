using DFTGames.Localization;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageToggle : MonoBehaviour
{
    public GameObject[] toggles;
    private ColorBlock theColor;

    // Start is called before the first frame update
    void Start()
    {
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

    public void setLanguage(string language)
    {
        switch (language)
        {
            case "English":
                Localize.SetCurrentLanguage(SystemLanguage.English);
                LocalizeImage.SetCurrentLanguage();
                PlayerPrefs.SetString("Language", "English");
                break;
            case "Croatian":
                Localize.SetCurrentLanguage(SystemLanguage.SerboCroatian);
                LocalizeImage.SetCurrentLanguage();
                PlayerPrefs.SetString("Language", "SerboCroatian");
                break;
            case "Spanish":
                Localize.SetCurrentLanguage(SystemLanguage.Spanish);
                LocalizeImage.SetCurrentLanguage();
                PlayerPrefs.SetString("Language", "Spanish");
                break;
            case "French":
                Localize.SetCurrentLanguage(SystemLanguage.French);
                LocalizeImage.SetCurrentLanguage();
                PlayerPrefs.SetString("Language", "French");
                break;
            case "Portugese":
                Localize.SetCurrentLanguage(SystemLanguage.Portuguese);
                LocalizeImage.SetCurrentLanguage();
                PlayerPrefs.SetString("Language", "Portuguese");
                break;
            case "Hungarian":
                Localize.SetCurrentLanguage(SystemLanguage.Hungarian);
                LocalizeImage.SetCurrentLanguage();
                PlayerPrefs.SetString("Language", "Hungarian");
                break;
        }
    }
}
