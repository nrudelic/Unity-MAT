using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class toNumberDiffStandard : MonoBehaviour
{
    public Slider slider;
    public void click()
    {
        PlayerPrefs.SetInt("toNumber", (int) slider.value);
    }
}
