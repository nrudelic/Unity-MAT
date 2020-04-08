using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LogoScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("loadMain", 2);
    }

    public void loadMain()
    {
        SceneManager.LoadScene(1);
    }
}
