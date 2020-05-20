using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderValue : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI text;
    public void changeValue()
    {
        text.text = slider.value.ToString();
    }
}
