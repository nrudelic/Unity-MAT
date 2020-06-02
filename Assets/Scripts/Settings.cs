using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;
using System.Security.Cryptography.X509Certificates;
using DFTGames.Localization;
using System.Linq;

public class Settings : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject settingsPanelObject;
    public GameObject[] menuButtons;
    public ToggleGroup NumbOfTasks;

    public TMP_InputField toNumberInput;
    
    private GameObject[] numOfTasksToggles;
    private GameObject[] numOfAnsToggles;
    private GameObject[] musicToggle;
    public GameObject[] operationToggles;
    public GameObject[] languageToggles;

    private GameObject numOfTasksGO;
    private GameObject numOfAnsGO;
    private GameObject musicGO;
    private GameObject typeAnsGO;
    
    private GameObject numOfTasksGONew;
    private GameObject numOfAnsGONew;
    private GameObject musicGONew;
    private GameObject typeOfAnswer;
    private GameObject typeOfAnswerNew;
    private GameObject languageEntry;

    public Toggle addition, substraction, multiplication, division;

    public GameObject[] typeOfAns;

    public GameObject music;
    public Slider slider;
    public TextMeshProUGUI sliderText;
    private int SliderValueOnEntry;

    private List<Toggle> operationEntry;
    private string newLanguage;
    
    public void settingsPanel()
    {
        music = Resources.FindObjectsOfTypeAll<AudioSource>()[0].gameObject;
        GameObject gameOverParent = GameObject.Find("Canvas");
        settingsPanelObject = gameOverParent.transform.Find("SettingsPanel").gameObject;
        sliderText.text = slider.value.ToString();
        if (!(settingsPanelObject is null)){
            settingsPanelObject.SetActive(true);
        }else{
            print("NULL");
        }
        int numOfTask = PlayerPrefs.GetInt("NumberOfTasks");
        numOfTasksToggles = GameObject.FindGameObjectsWithTag("NumberOfTasks");
        numOfAnsToggles = GameObject.FindGameObjectsWithTag("NumberOfAnswers");
        musicToggle = GameObject.FindGameObjectsWithTag("Music");

        highlightRightOne(PlayerPrefs.GetInt("NumberOfTasks"), PlayerPrefs.GetInt("NumberOfAnswers"), PlayerPrefs.GetInt("Music"), PlayerPrefs.GetString("Language"));

        NumberOfAnsToggle();
        NumberOfTasksToggle();
        MusicToggle();
        TypeOfAnswer();
        OperationsToggle();

        //Values on enter
        numOfAnsGO = whichIsOn(numOfAnsToggles);
        numOfTasksGO = whichIsOn(numOfTasksToggles);
        musicGO = whichIsOn(musicToggle);
        SliderValueOnEntry = (int) slider.value;
        typeAnsGO = whichIsOn(typeOfAns);
        languageEntry = whichIsOn(languageToggles);
        operationEntry = new List<Toggle>();

        if (addition.isOn) operationEntry.Add(addition);
        if (substraction.isOn) operationEntry.Add(substraction);
        if (multiplication.isOn) operationEntry.Add(multiplication);
        if (division.isOn) operationEntry.Add(division);
        
        
        menuButtons = GameObject.FindGameObjectsWithTag("MenuButton");
        foreach(GameObject button in menuButtons)
        {
            button.GetComponent<Button>().interactable = false;
        }
    }

    private void TypeOfAnswer()
    {
        foreach(GameObject typeToggle in typeOfAns){
            Toggle toggle = typeToggle.GetComponent<Toggle>();
            ColorBlock color = toggle.colors;
            if (toggle.isOn)
            {
                typeOfAnswer = typeToggle;
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

    public void highlightRightOne(int tasks, int answers, int music1, string language)
    {

        foreach(GameObject obj in numOfTasksToggles)
        {
            if(FindValueNumOfTasks(obj.name) == tasks)
            {
                obj.GetComponent<Toggle>().isOn = true;
                break;
            }
        }
        foreach (GameObject obj in numOfAnsToggles)
        {
            if (FindValueNumOfAns(obj.name) == answers)
            {
                obj.GetComponent<Toggle>().isOn = true;
                break;
            }
        }
        print(music);
        foreach (GameObject obj in musicToggle)
        {
            if (obj.name.Equals("MusicYes") && music.activeSelf)
            {
                obj.GetComponent<Toggle>().isOn = true;
                break;
            } else if (obj.name.Equals("MusicNo") && music.activeSelf)
            {
                obj.GetComponent<Toggle>().isOn = true;
                break;
            }
        }

        switch (language)
        {
            case "English":
                languageToggles[0].GetComponent<Toggle>().isOn = true;
                break;
            case "Croatian":
                languageToggles[1].GetComponent<Toggle>().isOn = true;
                break;
            case "Spanish":
                languageToggles[2].GetComponent<Toggle>().isOn = true;
                break;
            case "French":
                languageToggles[3].GetComponent<Toggle>().isOn = true;
                break;
            case "Portuguese":
                languageToggles[4].GetComponent<Toggle>().isOn = true;
                break;
            case "Hungarian":
                languageToggles[5].GetComponent<Toggle>().isOn = true;
                break;
        }

    }

    public void NumberOfTasksToggle()
    {
        foreach (GameObject toggleGameObj in numOfTasksToggles)
        {
            Toggle toggle = toggleGameObj.GetComponent<Toggle>();
            ColorBlock color = toggle.colors;
            if (toggle.isOn)
            {
                color.normalColor = Color.blue;
                color.pressedColor = Color.blue;
                color.highlightedColor = Color.blue;
                numOfTasksGONew = toggleGameObj;
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

    public void OperationsToggle()
    {
        foreach(GameObject toggleObj in operationToggles)
        {
            Toggle toggle = toggleObj.GetComponent<Toggle>();
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

    public void NumberOfAnsToggle()
    {
        foreach (GameObject toggleGameObj in numOfAnsToggles)
        {
            Toggle toggle = toggleGameObj.GetComponent<Toggle>();
            ColorBlock color = toggle.colors;
            if (toggle.isOn)
            {
                color.normalColor = Color.blue;
                color.pressedColor = Color.blue;
                color.highlightedColor = Color.blue;
                numOfAnsGONew = toggleGameObj;
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

    public void MusicToggle()
    {
        foreach (GameObject toggleGameObj in musicToggle)
        {
            Toggle toggle = toggleGameObj.GetComponent<Toggle>();
            ColorBlock color = toggle.colors;
            if (toggle.isOn)
            {
                color.normalColor = Color.blue;
                color.pressedColor = Color.blue;
                color.highlightedColor = Color.blue;
                musicGONew = toggleGameObj;
                if (toggleGameObj.name.Equals("MusicYes"))
                {
                    PlayerPrefs.SetInt("Music", 1);
                    print("Upali glazbu");
                }
                else
                {
                    PlayerPrefs.SetInt("Music", 0);
                }
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

    public void backToMenu()
    {
        numOfTasksGO.GetComponent<Toggle>().isOn = true;
        musicGO.GetComponent<Toggle>().isOn = true;
        numOfAnsGO.GetComponent<Toggle>().isOn = true;
        typeAnsGO.GetComponent<Toggle>().isOn = true;
        languageEntry.GetComponent<Toggle>().isOn = true;
        String language = PlayerPrefs.GetString("Language");
        print("Language: " + language);
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
        slider.value = SliderValueOnEntry;
        settingsPanelObject.SetActive(false);
        foreach (GameObject button in menuButtons)
        {
            button.GetComponent<Button>().interactable = true;
        }
        foreach(Toggle toggle in operationEntry)
        {
            toggle.isOn = true;
        }
    }

    public void SaveSettings()
    {
        MusicToggle();
        print("Music" +musicGONew.name);

        if (musicGONew.name.Equals("MusicYes"))
        {
            print("Active");
            PlayerPrefs.SetInt("Music", 1);
            music.SetActive(true);
        }
        else
        {
            PlayerPrefs.SetInt("Music", 0);
            if (music != null)
            {
                music.SetActive(false);
            }
        }

        int numberOfAnsInt = FindValueNumOfAns(numOfAnsGONew.name);
        PlayerPrefs.SetInt("NumberOfAnswers", numberOfAnsInt);

        int numberOfTasksInt = FindValueNumOfTasks(numOfTasksGONew.name);
        PlayerPrefs.SetInt("NumberOfTasks", numberOfTasksInt);

        settingsPanelObject.SetActive(false);
        foreach (GameObject button in menuButtons)
        {
            button.GetComponent<Button>().interactable = true;
        }

        PlayerPrefs.SetInt("toNumber", Int32.Parse(sliderText.text));
        TypeOfAnswer();
        FindTypeOfAnswer(typeOfAnswer.name);
        PlayerPrefs.SetInt("toNumber",(int) slider.value);
        //Getting viable operations
        if (addition.isOn)
        {
            PlayerPrefs.SetInt("Addition", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Addition", 0);
        }if (substraction.isOn)
        {
            PlayerPrefs.SetInt("Substraction", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Substraction", 0);
        }
        if (multiplication.isOn)
        {
            PlayerPrefs.SetInt("Multiplication", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Multiplication", 0);
        }
        if (division.isOn)
        {
            PlayerPrefs.SetInt("Division", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Division", 0);
        }
    }


    private int FindValueNumOfTasks(string name)
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

    private void FindTypeOfAnswer(String name)
    {
        if (name.Equals("NumbersOption"))
        {
            PlayerPrefs.SetString("typeOfAnswer", "Numbers");
        }
        else if (name.Equals("SymbolsOption"))
        {
            PlayerPrefs.SetString("typeOfAnswer", "Symbols");
        }
        else if (name.Equals("BlocksOption"))
        {
            PlayerPrefs.SetString("typeOfAnswer", "Blocks");
        }
    }

    private int FindValueNumOfAns(string name)
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

    private GameObject whichIsOn(GameObject[] objects)
    {
        foreach(GameObject obj in objects)
        {
            if (obj.GetComponent<Toggle>().isOn)
            {
                return obj;
            }
        }
        return null;
    }
}
