using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace DFTGames.Localization
{
    public static class Locale
    {
        const string STR_LOCALIZATION_KEY = "locale";
        const string STR_LOCALIZATION_PREFIX = "localization/";
        static string currentLanguage;
        //static bool currentLanguageFileHasBeenFound = false;
        public static bool currentLanguageHasBeenSet = false;
        public static Dictionary<string, string> CurrentLanguageStrings = new Dictionary<string, string>();
        static TextAsset currentLocalizationText;

        /// <summary>
        /// This sets the current language. It expects a standard .Net CultureInfo.Name format
        /// </summary>
        public static string CurrentLanguage
        {
            get { return currentLanguage; }
            set
            {
                if (value != null && value.Trim() != string.Empty)
                {
                    currentLanguage = value;
                    if (value.Equals(SystemLanguage.French.ToString()))
                    {
                        currentLanguage = SystemLanguage.French.ToString();
                    }
                    else if (value.Equals(SystemLanguage.English.ToString()))
                    {
                        currentLanguage = SystemLanguage.English.ToString();
                    }else if (value.Equals(SystemLanguage.SerboCroatian.ToString()))
                    {
                        currentLanguage = SystemLanguage.SerboCroatian.ToString();
                    }else if (value.Equals(SystemLanguage.Spanish.ToString()))
                    {
                        currentLanguage = SystemLanguage.Spanish.ToString();
                    }
                    else
                    {
                        currentLanguage = SystemLanguage.English.ToString();
                    }

                    currentLanguageHasBeenSet = true;

                    Debug.Log(STR_LOCALIZATION_PREFIX + currentLanguage);
                    currentLocalizationText = Resources.Load(STR_LOCALIZATION_PREFIX + currentLanguage, typeof(TextAsset)) as TextAsset;

                    if (currentLocalizationText == null)
                    {
                        currentLanguage = SystemLanguage.English.ToString();
                        currentLocalizationText = Resources.Load(STR_LOCALIZATION_PREFIX + currentLanguage, typeof(TextAsset)) as TextAsset;
                    }
                    if (currentLocalizationText != null)
                    {
                        string[] lines = currentLocalizationText.text.Split(new string[] { "\r\n", "\n\r", "\n" }, System.StringSplitOptions.RemoveEmptyEntries);
                        CurrentLanguageStrings.Clear();
                        for (int i = 0; i < lines.Length; i++)
                        {
                            string[] pairs = lines[i].Split(new char[] { '\t', '=' }, 2);
                            if (pairs.Length == 2)
                            {
                                CurrentLanguageStrings.Add(pairs[0].Trim(), pairs[1].Trim());
                            }
                        }
                    }
                    else
                    {
                        Debug.LogErrorFormat("Locale Language '{0}' not found!", currentLanguage);
                    }
                }
            }
        }


        public static bool CurrentLanguageHasBeenSet
        {
            get
            {
                return currentLanguageHasBeenSet;
            }
            set
            {
                currentLanguageHasBeenSet = value;
            }
        }

        /// <summary>
        /// The player language. If not set in PlayerPrefs then returns Application.systemLanguage
        /// </summary>
        public static SystemLanguage PlayerLanguage
        {
            get
            {
                return (SystemLanguage)PlayerPrefs.GetInt(STR_LOCALIZATION_KEY, (int)Application.systemLanguage);
            }
            set
            {
                PlayerPrefs.SetInt(STR_LOCALIZATION_KEY, (int)value);
                PlayerPrefs.Save();
            }
        }
    }
}