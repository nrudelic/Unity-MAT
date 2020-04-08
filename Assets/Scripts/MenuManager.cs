using System.Collections;
using System.Collections.Generic;
using DFTGames.Localization;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public void SetEnglish()
    {
        print("ENG");
        Localize.SetCurrentLanguage(SystemLanguage.English);
        LocalizeImage.SetCurrentLanguage();
    }

    public void SetSpanish()
    {
        print("SPAN");
        Localize.SetCurrentLanguage(SystemLanguage.Spanish);
        LocalizeImage.SetCurrentLanguage();
    }

    public void SetSerboCroatian()
    {
        print("SPAN");
        Localize.SetCurrentLanguage(SystemLanguage.SerboCroatian);
        LocalizeImage.SetCurrentLanguage();
    }

    public void SetFrench()
    {
        print("SPAN");
        Localize.SetCurrentLanguage(SystemLanguage.French);
        LocalizeImage.SetCurrentLanguage();
    }

}
