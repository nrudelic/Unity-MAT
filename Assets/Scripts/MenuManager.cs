using System.Collections;
using System.Collections.Generic;
using DFTGames.Localization;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    private void Start()
    {

        string language = PlayerPrefs.GetString("Language");
        switch (language)
        {
            case "English":
                Localize.SetCurrentLanguage(SystemLanguage.English);
                LocalizeImage.SetCurrentLanguage();
                break;
            case "Croatian":
                Localize.SetCurrentLanguage(SystemLanguage.SerboCroatian);
                LocalizeImage.SetCurrentLanguage();
                break;
            case "Spanish":
                Localize.SetCurrentLanguage(SystemLanguage.Spanish);
                LocalizeImage.SetCurrentLanguage();
                break;
            case "French":
                Localize.SetCurrentLanguage(SystemLanguage.French);
                LocalizeImage.SetCurrentLanguage();
                break;
            case "Portuguese":
                Localize.SetCurrentLanguage(SystemLanguage.Portuguese);
                LocalizeImage.SetCurrentLanguage();
                break;
            case "Hungarian":
                Localize.SetCurrentLanguage(SystemLanguage.Hungarian);
                LocalizeImage.SetCurrentLanguage();
                break;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
