using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Settings : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject settingsPanelObject;
    public GameObject[] menuButtons;
    public ToggleGroup NumbOfTasks;

    private GameObject[] numOfTasksToggles;
    private GameObject[] numOfAnsToggles;
    private GameObject[] musicToggle;

    private GameObject numOfTasksGO;
    private GameObject numOfAnsGO;
    private GameObject musicGO;
    
    private GameObject numOfTasksGONew;
    private GameObject numOfAnsGONew;
    private GameObject musicGONew;

    public GameObject music;

    public void settingsPanel()
    {
        GameObject gameOverParent = GameObject.Find("Canvas");
        settingsPanelObject = gameOverParent.transform.Find("SettingsPanel").gameObject;

        if (!(settingsPanelObject is null)){
            settingsPanelObject.SetActive(true);
        }else{
            print("NULL");
        }
        int numOfTask = PlayerPrefs.GetInt("NumberOfTasks");
        numOfTasksToggles = GameObject.FindGameObjectsWithTag("NumberOfTasks");
        numOfAnsToggles = GameObject.FindGameObjectsWithTag("NumberOfAnswers");
        musicToggle = GameObject.FindGameObjectsWithTag("Music");

        highlightRightOne(PlayerPrefs.GetInt("NumberOfTasks"), PlayerPrefs.GetInt("NumberOfAnswers"), PlayerPrefs.GetInt("Music"));

        NumberOfAnsToggle();
        NumberOfTasksToggle();
        MusicToggle();

        //Values on enter
        //numOfAnsGO = whichIsOn(numOfAnsToggles);
        //numOfTasksGO = whichIsOn(numOfTasksToggles);
        //musicGO = whichIsOn(musicToggle);



        menuButtons = GameObject.FindGameObjectsWithTag("MenuButton");
        foreach(GameObject button in menuButtons)
        {
            button.GetComponent<Button>().interactable = false;
        }
    }

    public void highlightRightOne(int tasks, int answers, int music)
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
        foreach (GameObject obj in musicToggle)
        {
            if (obj.name.Equals("MusicYes") && music == 1)
            {
                obj.GetComponent<Toggle>().isOn = true;
                break;
            } else
            if (obj.name.Equals("MusicNo") && music == 0)
            {
                obj.GetComponent<Toggle>().isOn = true;
                break;
            }
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
                }
                else
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

    public void backToMenu()
    {
        numOfTasksGO.GetComponent<Toggle>().isOn = true;
        musicGO.GetComponent<Toggle>().isOn = true;
        numOfAnsGO.GetComponent<Toggle>().isOn = true;

        settingsPanelObject.SetActive(false);
        foreach (GameObject button in menuButtons)
        {
            button.GetComponent<Button>().interactable = true;
        }
    }

    public void SaveSettings()
    {
        if (musicGONew.name.Equals("MusicYes"))
        {
            PlayerPrefs.SetInt("Music", 1);
            music.SetActive(true);
        }
        else
        {
            PlayerPrefs.SetInt("Music", 0);
            music.SetActive(false);
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
