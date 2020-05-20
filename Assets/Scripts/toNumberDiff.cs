using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toNumberDiff : MonoBehaviour
{
    // Start is called before the first frame update
    public void click()
    {
        PlayerPrefs.SetInt("toNumber", 10);
    }
}
