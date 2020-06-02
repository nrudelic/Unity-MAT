using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;
using System.Diagnostics;
using UnityEngine.SceneManagement;

public class NumbersGame : MonoBehaviour
{
    public GameObject canvas;
    public GameObject image1;
    public GameObject image2;
    public GameObject image3;
    public GameObject image4;
    public GameObject image5;
    public GameObject image6;
    public GameObject image7;
    public GameObject image8;
    public GameObject image9;
    public GameObject image10;
    public GameObject sunImg;
    public static List<GameObject> images;
    public static int start = 1;
    public GameObject[] signs;
    public static GameObject rightButton;
    public Image answerColorImage;
    public static TextMeshProUGUI pressed;
    public GameObject answerFrame;
    private static Vector3 destination;
    private int shouldMove = 0;
    private static List<GameObject> triedAnswers;
    private static int goNext = 0;
    
    private int roundsPlayed;
    private int maxRounds;
    private Stopwatch timer;

    private static int numAnswers;
    public AudioSource correct;
    public AudioSource wrong;

    // Start is called before the first frame update
    void Start()
    {
        roundsPlayed = 0;
        timer = new Stopwatch();
        timer.Start();
        maxRounds = PlayerPrefs.GetInt("NumberOfTasks");
        destination = answerFrame.transform.position;
        numAnswers = PlayerPrefs.GetInt("NumberOfAnswers");
        PlayerPrefs.SetInt("NumberOfRightAnswers", 0);
        PlayerPrefs.SetInt("NumberOfWrongAnswers", 0);
        images = new List<GameObject>();
        images.Add(image1);
        images.Add(image2);
        images.Add(image3);
        images.Add(image4);
        images.Add(image5);
        images.Add(image6);
        images.Add(image7);
        images.Add(image8);
        images.Add(image9);
        images.Add(image10);
        loadNextQuestion();
        //Update();
    }


    private void loadNextQuestion()
    {
        answerColorImage.color = new Color32(255, 255, 255, 0);
        foreach (GameObject obj in images)
        {
            obj.SetActive(false);
        }
        start = 0;
        int nextRandomAnswer = UnityEngine.Random.Range(0, 10) + 1;

        pressed = null;

        int nextRandomPicture = UnityEngine.Random.Range(0, 21) + 1;

        triedAnswers = new List<GameObject>();

        renderImages(nextRandomAnswer);
        renderSigns(nextRandomAnswer);
    }

    private void renderSigns(int nextRandomAnswer)
    {
        List<int> answers = new List<int>();


        for (int i = 0; i < numAnswers - 1; i++)
        {
            int random = UnityEngine.Random.Range(0, 10) + 1;
            while (answers.Contains(random) || random == nextRandomAnswer)
            {
                random = UnityEngine.Random.Range(0, 10) + 1;
            }
            answers.Add(random);
        }

        int minMargin = 20;
        int minWidth = 140;
        int screenWidth = Screen.width;

        float canvasX = canvas.GetComponent<Canvas>().transform.position.x;
        float width = Mathf.Min(minWidth, (screenWidth - minMargin * (numAnswers + 1)) / numAnswers);
        float height = width - 25;
        float margin = Mathf.Max((screenWidth - width * numAnswers) / (numAnswers + 1), minMargin);

        float leftMargin = (screenWidth - numAnswers * (width)) / (numAnswers + 1);
        int leftBoundary = (screenWidth / 2) * (-1);

        for (int index = 0; index < numAnswers; index++)
        {
            Button button = signs[index].GetComponent<Button>();
            button.image.rectTransform.sizeDelta = new Vector2(width, height);
            button.image.gameObject.SetActive(true);
            button.interactable = true;
            button.transform.GetChild(0).gameObject.SetActive(true);
            Vector3 imgPos = button.image.rectTransform.position;
            Vector3 newPosition = new Vector3(leftBoundary + width / 2 + leftMargin * (index + 1) + index * (width) + canvasX, imgPos.y, imgPos.z);
            button.image.rectTransform.position = newPosition;
            button.transform.GetChild(0).gameObject.transform.position = newPosition;
        }

        int randomRightSign = UnityEngine.Random.Range(0, numAnswers);
        GameObject rightSign = signs[randomRightSign];

        rightButton = signs[randomRightSign];

        rightSign.GetComponentInChildren<TextMeshProUGUI>().SetText("" + nextRandomAnswer);
        List<GameObject> wrongSigns = new List<GameObject>();

        for (int i = 0, k = 0; i < numAnswers; i++)
        {
            if (i != randomRightSign)
            {
                wrongSigns.Add(signs[i]);
                int wrongAns = answers[k++];
                signs[i].GetComponentInChildren<TextMeshProUGUI>().SetText("" + wrongAns);
            }
        }
    }

    private void renderImages(int nextRandomAnswer)
    {
        int minMargin = 30;
        int maxWidth = 120;
        int screenWidth = Screen.width;
        float canvasX = canvas.GetComponent<Canvas>().transform.position.x;
        //print("Screen: " + screenWidth);

        //float width = Mathf.Min(maxWidth, (screenWidth - sunImg.GetComponent<Image>().rectTransform.sizeDelta.x - minMargin * (nextRandomAnswer+1))/ nextRandomAnswer);
        float width = Mathf.Min(maxWidth, (screenWidth - minMargin * (nextRandomAnswer + 1)) / nextRandomAnswer);
        float height = width;
        //float margin = Mathf.Max((screenWidth - sunImg.GetComponent<Image>().rectTransform.sizeDelta.x - width * nextRandomAnswer) / (nextRandomAnswer + 1), minMargin);
        float margin = Mathf.Max((screenWidth - width * nextRandomAnswer) / (nextRandomAnswer + 1), minMargin);

        float leftSpace = (screenWidth - nextRandomAnswer * (width + margin)) / 2;

        //float leftMargin = ((screenWidth-sunImg.GetComponent<Image>().rectTransform.sizeDelta.x) - nextRandomAnswer * (width)) / (nextRandomAnswer + 1);
        float leftMargin = ((screenWidth - nextRandomAnswer * (width)) / (nextRandomAnswer + 1));
        int leftBoundary = (screenWidth / 2) * (-1);

        //print("Width: " + width);
        //print("margin : " + margin);
        //print("Left Boundary:" + leftBoundary);
        //print("Left marign: " + leftMargin);
        for (int index = 0; index < nextRandomAnswer; index++)
        {
            Image image = images[index].GetComponent<Image>();
            image.rectTransform.sizeDelta = new Vector2(width, height);
            image.gameObject.SetActive(true);
            Vector3 imgPos = image.rectTransform.position;
            Vector3 newPosition = new Vector3(leftBoundary + width / 2 + leftMargin * (index + 1) + index * (width) + canvasX, imgPos.y, imgPos.z);
            //print("x: " + newPosition.x + " y: " + newPosition.y + " z: " + newPosition.z);
            image.rectTransform.position = newPosition;
        }
    }

    public void onClick(GameObject pressedButton)
    {
        
        foreach(GameObject obj in signs)
        {
            obj.GetComponent<Button>().interactable = false;
        }
        
        if (pressed != null)
        {
            pressed.gameObject.SetActive(false);
        }

        pressed = pressedButton.GetComponentInChildren<TextMeshProUGUI>();


        if (pressedButton.Equals(rightButton))
        {
            if (triedAnswers.Count == 0)
            {
                PlayerPrefs.SetInt("NumberOfRightAnswers", PlayerPrefs.GetInt("NumberOfRightAnswers") + 1);
            }
            print("tocno");
            goNext = 1;
            answerColorImage.color = new Color32(4, 161, 14, 91);
            shouldMove = 1;
            correct.Play();
        } else {
            if (triedAnswers.Count == 0)
            {
                PlayerPrefs.SetInt("NumberOfWrongAnswers", PlayerPrefs.GetInt("NumberOfWrongAnswers") + 1);
            }
            triedAnswers.Add(pressedButton);
            answerColorImage.color = new Color32(161, 26, 4, 121);
            //pressedButton.SetActive(false);
            Image img = pressedButton.GetComponent<Image>();
            shouldMove = 1;
            wrong.Play();
            pressedButton.GetComponent<Button>().interactable = false;
        }
    }

    void IncrementPosition()
    {
        // Calculate the next position
        float speed = 500;
        float delta = speed * Time.deltaTime;
        Vector3 currentPosition = pressed.rectTransform.position;
        Vector3 nextPosition = Vector3.MoveTowards(currentPosition, destination, delta);

        // Move the object to the next position
        pressed.rectTransform.position = nextPosition;
    }

    void Update()
    {
        // If the object is not at the target destination
        if (shouldMove == 1)
        {
            if (destination != pressed.rectTransform.position)
            {
                print("micem");
                // Move towards the destination each frame until the object reaches it
                IncrementPosition();
            }
            else
            {
                print("gotov");
                if(goNext == 1)
                {
                    goNext = 0;
                    shouldMove = 0;
                    roundsPlayed++;
                    if(roundsPlayed >= maxRounds && maxRounds != 0)
                    {
                        timer.Stop();
                        TimeSpan elapsed = timer.Elapsed;
                        PlayerPrefs.SetString("TotalTime", "" + elapsed.Minutes + ":" + elapsed.Seconds);
                        PlayerPrefs.SetInt("NextScene", 2);
                        SceneManager.LoadScene(5);
                    } else
                    {
                        loadNextQuestion();
                    }
                }
                foreach(GameObject obj in signs)
                {
                    if (!triedAnswers.Contains(obj))
                    {
                        obj.GetComponent<Button>().interactable = true;
                    }
                }
                shouldMove = 0;
            }

        }
    }

}
