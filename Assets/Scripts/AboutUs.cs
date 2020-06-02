using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;

public class AboutUs : MonoBehaviour
{
    public GameObject panel;
    public void OpenAbout()
    {
        panel.SetActive(true);
    }

    public void CloseAbout()
    {
        panel.SetActive(false);
    }
}
