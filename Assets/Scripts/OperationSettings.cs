using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OperationSettings : MonoBehaviour
{
    public Toggle addition, substraction, multiplication, division;
    private int noChecked;
    public GameObject[] operationToggles;
    
    void countChecked()
    {
        noChecked = 0;
        if (addition.isOn) noChecked++;
        if (substraction.isOn) noChecked++;
        if (multiplication.isOn) noChecked++;
        if (division.isOn) noChecked++;
        
    }
    public void onChange(Toggle pressed)
    {
        countChecked();
        if(noChecked == 0 && pressed.isOn == false)
        {
            pressed.isOn = true;
        }
        OperationsToggle();
    }

    public void OperationsToggle()
    {
        foreach (GameObject toggleObj in operationToggles)
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
}
