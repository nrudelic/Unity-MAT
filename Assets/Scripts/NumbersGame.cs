using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class NumbersGame : MonoBehaviour
{
    public static TextMeshProUGUI signOneText;
    public static TextMeshProUGUI signTwoText;
    public GameObject image1;
    public GameObject image2;
    public GameObject image3;
    public GameObject image4;
    public GameObject image5;
    public GameObject image6;
    public static List<GameObject> images;

    // Start is called before the first frame update
    void Start()
    {
        signOneText = GameObject.Find("Canvas/Sign1/Sign1Button/Sign1Text").GetComponent<TextMeshProUGUI>();
        signTwoText = GameObject.Find("Canvas/Sign2/Sign2Button/Sign2Text").GetComponent<TextMeshProUGUI>();

       
        //images.Add(panel.transform.FindChild("Image1").gameObject.GetComponent<Image>());
        // images.Add(panel.transform.FindChild("Image2").gameObject.GetComponent<Image>());
        // images.Add(panel.transform.FindChild("Image3").gameObject.GetComponent<Image>());
        //  images.Add(panel.transform.FindChild("Image4").gameObject.GetComponent<Image>());
        // images.Add(panel.transform.FindChild("Image5").gameObject.GetComponent<Image>());
        // images.Add(panel.transform.FindChild("Image6").gameObject.GetComponent<Image>());

        startGame();
    }

    public void startGame()
    {
        int nextRandomAnswer = Random.Range(0, 5) + 1;
        int nextRandomWrong = nextRandomAnswer;
        while(nextRandomWrong == nextRandomAnswer)
        {
            nextRandomWrong = Random.Range(0, 10) + 1;
        }
        print(images);
        print(nextRandomAnswer);
        image3.SetActive(true);

        int nextRandomPicture = Random.Range(0, 21) + 1;
        signOneText.text = nextRandomAnswer.ToString();
        signTwoText.text = nextRandomWrong.ToString();
        print(nextRandomAnswer);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            int nextRandomAnswer = Random.Range(0, 10) + 1;
            int nextRandomWrong = nextRandomAnswer;
            while (nextRandomWrong == nextRandomAnswer)
            {
                nextRandomWrong = Random.Range(0, 10) + 1;
            }

            int nextRandomPicture = Random.Range(0, 21) + 1;
            signOneText.text = nextRandomAnswer.ToString();
            signTwoText.text = nextRandomWrong.ToString();
            print(nextRandomAnswer);
        }
    }
}
