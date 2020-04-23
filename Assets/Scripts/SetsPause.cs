using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetsPause : MonoBehaviour
{
    public GameObject[] buttons;
    public GameObject pausePanelObject;
    public void pausePanel()
    {
        pausePanelObject.SetActive(true);
        foreach(GameObject button in buttons)
        {
            button.GetComponent<Button>().interactable = false;
        }
    }

    public void unPause()
    {
        pausePanelObject.SetActive(false);
        foreach (GameObject button in buttons)
        {
            button.GetComponent<Button>().interactable = true;
        }
    }
}
