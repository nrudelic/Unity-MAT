﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{

    public GameObject pausePanelObject;
    public GameObject[] buttons;

    public void pausePanel()
    {
        GameObject parent = GameObject.FindGameObjectWithTag("NumberCanvas");
        pausePanelObject = parent.transform.Find("PausePanel").gameObject;

        pausePanelObject.SetActive(true);

        buttons = GameObject.FindGameObjectsWithTag("SignButtons");
        foreach(GameObject button in buttons)
        {
            button.GetComponent<Button>().interactable = false;
        }
    }

    public void unPause()
    {
        pausePanelObject.SetActive(false);
        foreach(GameObject button in buttons)
        {
            button.GetComponent<Button>().interactable = true;
        }
    }
}
