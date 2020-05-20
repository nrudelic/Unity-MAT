using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SetsGame : MonoBehaviour
{
    public GameObject answerFrame;
    public GameObject symbol;
    public GameObject canvas;
    public Image answerColorImage;
    public GameObject signEqual;
    public GameObject signNotEqual;
    public GameObject[] symbolsLeft;
    public GameObject[] symbolsRight;
    private GameObject pressed;

    private static readonly int UPPER_BOUND = 11;
    private static readonly int NO_OF_IMAGES = 21;

    private float canvasWidth;
    private float canvasHeight;
    private int shouldMove = 0;


    private static List<GameObject> canvasSymbols;

    private bool answer;
    private Image movingImage;
    private Vector3 destination;
    private Vector3 signEqualPosition;
    private Vector3 signNotEqualPosition;
    private int goNext = 0;


    private int roundsPlayed;
    private int maxRounds;
    private Stopwatch timer;
    private bool answered;

    public void MyStarter()
    {
        resetScene();
        startGame();
    }
    // Start is called before the first frame update
    void Start()
    {
        roundsPlayed = 0;
        timer = new Stopwatch();
        timer.Start();
        maxRounds = PlayerPrefs.GetInt("NumberOfTasks");
        PlayerPrefs.SetInt("NumberOfRightAnswers", 0);
        PlayerPrefs.SetInt("NumberOfWrongAnswers", 0);
        signEqualPosition = signEqual.transform.GetChild(0).gameObject.transform.position;
        signNotEqualPosition = signNotEqual.transform.GetChild(0).gameObject.transform.position;
        canvasSymbols = new List<GameObject>();
        canvasWidth = Screen.width;
        canvasHeight = Screen.height;
        destination = answerFrame.transform.position;

        startGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldMove == 1)
        {
            if (destination != movingImage.rectTransform.position)
            {
                // Move towards the destination each frame until the object reaches it
                IncrementPosition();
            }
            else
            {
                if (goNext == 1)
                {
                    goNext = 0;
                    shouldMove = 0;
                    roundsPlayed++;
                    if (roundsPlayed >= maxRounds && maxRounds != 0)
                    {
                        timer.Stop();
                        TimeSpan elapsed = timer.Elapsed;
                        PlayerPrefs.SetString("TotalTime", "" + elapsed.Minutes + ":" + elapsed.Seconds);
                        PlayerPrefs.SetInt("NextScene", 3);
                        SceneManager.LoadScene(5);
                    }
                    else
                    {
                        MyStarter();
                        return;
                    }
                }
                if (pressed.name.Equals("SignEqual"))
                {
                    signNotEqual.GetComponent<Button>().interactable = true;
                }
                else
                {
                    signEqual.GetComponent<Button>().interactable = true;

                }
                pressed.GetComponent<Button>().interactable = false;
                shouldMove = 0;
            }

        }
    }

    void IncrementPosition()
    {
        // Calculate the next position
        float speed = 400;
        float delta = speed * Time.deltaTime;
        Vector3 currentPosition = movingImage.rectTransform.position;
        Vector3 nextPosition = Vector3.MoveTowards(currentPosition, destination, delta);

        // Move the object to the next position
        movingImage.rectTransform.position = nextPosition;
    }

    private void resetScene()
    {
        if (canvasSymbols.Count != 0)
        {
            foreach (GameObject obj in canvasSymbols)
            {
                Destroy(obj);
            }
        }
        foreach (GameObject symbol in symbolsRight)
        {
            symbol.SetActive(false);
        }
        foreach (GameObject symbol in symbolsLeft)
        {
            symbol.SetActive(false);
        }
        signEqual.transform.GetChild(0).gameObject.GetComponent<Image>().transform.position = signEqualPosition;
        signNotEqual.transform.GetChild(0).gameObject.GetComponent<Image>().transform.position = signNotEqualPosition;
        signEqual.transform.GetChild(0).gameObject.GetComponent<Image>().gameObject.SetActive(true);
        signNotEqual.transform.GetChild(0).gameObject.GetComponent<Image>().gameObject.SetActive(true);
        signEqual.GetComponent<Button>().interactable = true;
        signNotEqual.GetComponent<Button>().interactable = true;
        movingImage = null;
        answerColorImage.color = new Color32(255, 255, 255, 0);
    }

    private void setSymbolImage()
    {
        int random = UnityEngine.Random.Range(0, NO_OF_IMAGES) + 1;
        Sprite newImage = Resources.Load<Sprite>("Symbols/symbol_" + random.ToString());
        foreach (GameObject symbol in symbolsLeft)
        {
            symbol.GetComponent<Image>().sprite = newImage;
        }
        foreach (GameObject symbol in symbolsRight)
        {
            symbol.GetComponent<Image>().sprite = newImage;
        }
    }

    private void startGame()
    {
        answered = false;
        setSymbolImage();
        int rightSymbols;
        int leftSymbols = UnityEngine.Random.Range(0, 11) + 1;
        int num = UnityEngine.Random.Range(0, 5) + 1;
        if (num % 2 == 0)
        {
            rightSymbols = leftSymbols;
            answer = true;
        }
        else
        {
            answer = false;
            rightSymbols = UnityEngine.Random.Range(0, 11) + 1;
            while (rightSymbols == leftSymbols)
            {
                rightSymbols = UnityEngine.Random.Range(0, 11) + 1;
            }
        }

        print("Left: " + leftSymbols);
        print("Right: " + rightSymbols);
        print(answer);

        //renderLeft(leftSymbols);
        //renderRight(rightSymbols);
        renderWithFixedSymbols(leftSymbols, symbolsLeft, 0);
        renderWithFixedSymbols(rightSymbols, symbolsRight, 1);
    }

    private void renderWithFixedSymbols(int numberOfSymbols, GameObject[] symbols, int side)
    {
        if (numberOfSymbols == 1)
        {
            symbols[7].SetActive(true);
        }
        else if (numberOfSymbols == 2)
        {
            if (side == 0)
            {
                symbols[6].SetActive(true);
                symbols[8].SetActive(true);
            }
            else
            {
                symbols[5].SetActive(true);
                symbols[6].SetActive(true);
            }
        }
        else if (numberOfSymbols == 3)
        {
            if (side == 0)
            {
                symbols[6].SetActive(true);
                symbols[7].SetActive(true);
                symbols[8].SetActive(true);
            }
            else
            {
                symbols[3].SetActive(true);
                symbols[7].SetActive(true);
                symbols[10].SetActive(true);
            }
        }
        else if (numberOfSymbols == 4)
        {
            if (side == 0)
            {
                symbols[0].SetActive(true);
                symbols[2].SetActive(true);
                symbols[6].SetActive(true);
                symbols[8].SetActive(true);
            }
            else
            {

                symbols[1].SetActive(true);
                symbols[3].SetActive(true);
                symbols[5].SetActive(true);
                symbols[7].SetActive(true);

            }
        }
        else if (numberOfSymbols == 5)
        {
            if (side == 0)
            {
                symbols[0].SetActive(true);
                symbols[2].SetActive(true);
                symbols[4].SetActive(true);
                symbols[6].SetActive(true);
                symbols[8].SetActive(true);
            }
            else
            {

                symbols[1].SetActive(true);
                symbols[3].SetActive(true);
                symbols[4].SetActive(true);
                symbols[5].SetActive(true);
                symbols[7].SetActive(true);
            }
        }
        else if (numberOfSymbols == 6)
        {
            if (side == 0)
            {
                symbols[0].SetActive(true);
                symbols[1].SetActive(true);
                symbols[2].SetActive(true);
                symbols[6].SetActive(true);
                symbols[7].SetActive(true);
                symbols[8].SetActive(true);
            }
            else
            {
                symbols[1].SetActive(true);
                symbols[3].SetActive(true);
                symbols[5].SetActive(true);
                symbols[7].SetActive(true);
                symbols[9].SetActive(true);
                symbols[10].SetActive(true);
            }
        }
        else if (numberOfSymbols == 7)
        {
            if (side == 0)
            {
                symbols[0].SetActive(true);
                symbols[1].SetActive(true);
                symbols[2].SetActive(true);
                symbols[4].SetActive(true);
                symbols[6].SetActive(true);
                symbols[7].SetActive(true);
                symbols[8].SetActive(true);
            }
            else
            {
                symbols[0].SetActive(true);
                symbols[2].SetActive(true);
                symbols[4].SetActive(true);
                symbols[6].SetActive(true);
                symbols[8].SetActive(true);
                symbols[9].SetActive(true);
                symbols[10].SetActive(true);

            }
        }
        else if (numberOfSymbols == 8)
        {
            if (side == 0)
            {
                symbols[0].SetActive(true);
                symbols[1].SetActive(true);
                symbols[2].SetActive(true);
                symbols[3].SetActive(true);
                symbols[4].SetActive(true);
                symbols[5].SetActive(true);
                symbols[6].SetActive(true);
                symbols[8].SetActive(true);
            }
            else
            {
                symbols[0].SetActive(true);
                symbols[1].SetActive(true);
                symbols[2].SetActive(true);
                symbols[3].SetActive(true);
                symbols[7].SetActive(true);
                symbols[5].SetActive(true);
                symbols[6].SetActive(true);
                symbols[8].SetActive(true);

            }
        }
        else if (numberOfSymbols == 9)
        {
            if (side == 0)
            {
                symbols[0].SetActive(true);
                symbols[1].SetActive(true);
                symbols[2].SetActive(true);
                symbols[3].SetActive(true);
                symbols[4].SetActive(true);
                symbols[5].SetActive(true);
                symbols[6].SetActive(true);
                symbols[7].SetActive(true);
                symbols[8].SetActive(true);
            }
            else
            {

                symbols[0].SetActive(true);
                symbols[9].SetActive(true);
                symbols[2].SetActive(true);
                symbols[3].SetActive(true);
                symbols[4].SetActive(true);
                symbols[5].SetActive(true);
                symbols[6].SetActive(true);
                symbols[10].SetActive(true);
                symbols[8].SetActive(true);

            }
        }
        else if (numberOfSymbols == 10)
        {
            symbols[0].SetActive(true);
            symbols[1].SetActive(true);
            symbols[2].SetActive(true);
            symbols[3].SetActive(true);
            symbols[4].SetActive(true);
            symbols[5].SetActive(true);
            symbols[6].SetActive(true);
            symbols[9].SetActive(true);
            symbols[8].SetActive(true);
            symbols[10].SetActive(true);
        }
        else if (numberOfSymbols == 11)
        {
            foreach (GameObject symb in symbols)
            {
                symb.SetActive(true);
            }
        }



    }
    private void renderLeft(int symbols)
    {
        int minMargin = 30;
        int maxWidth = 100;
        float answerWidth = answerFrame.GetComponent<RectTransform>().rect.width;


        float canvasX = canvas.GetComponent<Canvas>().transform.position.x - canvasWidth / 2;
        float canvasY = canvas.GetComponent<Canvas>().transform.position.y;
        float widthSpace = (canvasWidth / 2) - answerWidth / 2;

        float width = Mathf.Min(maxWidth, (widthSpace - minMargin * (symbols % 4)) / (symbols % 4));
        //float width = Mathf.Min(maxWidth, (canvasWidth/2 - minMargin * 4) / 3);
        float height = width;

        for (int i = 0; i < symbols; i++)
        {
            GameObject newSymbol = Instantiate(symbol);
            newSymbol.transform.SetParent(canvas.transform, false);

            float positionX = 0;
            float positionY = 0;
            canvasSymbols.Add(newSymbol);
            if (symbols == 1)
            {
                positionX = widthSpace / 2 + canvasX;
                positionY = canvasY;
            }
            else if (symbols <= 3)
            {
                float margin = Mathf.Max((widthSpace - width * symbols - width / 2) / (symbols + 1), minMargin);
                positionX = canvasX + margin * (i + 1) + width * i + width / 2;
                positionY = canvasHeight / 3 + canvasHeight / 6 + UnityEngine.Random.Range(-20, 100);
            }
            else if (symbols == 4)
            {
                float margin = Mathf.Max((widthSpace - width * 2 - width / 2) / 3, minMargin);
                positionX = canvasX + width / 2 + margin * ((i % 2) + 1) + width * (i % 2);
                if (i < 2)
                {
                    positionY = 2 * canvasHeight / 3 + (canvasHeight / 6) + UnityEngine.Random.Range(-50, 50);

                }
                else
                {

                    positionY = canvasHeight / 3 + (canvasHeight / 6) + UnityEngine.Random.Range(-50, 50);
                }
            }
            else if (symbols < 9)
            {
                positionX = canvasX + width / 2 + (i % 3) * (widthSpace / 3) + UnityEngine.Random.Range(0, widthSpace / 3 - width + 20);
                positionY = canvasHeight - height / 2 - ((i / 3) * (2 * canvasHeight / (3 * 3))) - UnityEngine.Random.Range(0, (2 * canvasHeight / (3 * 3)) - height + 20);
            }

            newSymbol.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(width - 20, height - 20);
            newSymbol.transform.position = new Vector3(positionX, positionY);
            newSymbol.SetActive(true);
        }
    }

    private void renderRight(int symbols)
    {
        int minMargin = 30;
        int maxWidth = 100;
        float answerWidth = answerFrame.GetComponent<RectTransform>().rect.width;


        float canvasX = canvas.GetComponent<Canvas>().transform.position.x;
        float canvasY = canvas.GetComponent<Canvas>().transform.position.y;
        float widthSpace = (canvasWidth / 2) - answerWidth / 2;

        float width = Mathf.Min(maxWidth, (widthSpace - minMargin * (symbols % 4)) / (symbols % 4));
        //float width = Mathf.Min(maxWidth, (canvasWidth/2 - minMargin * 4) / 3);
        float height = width;

        for (int i = 0; i < symbols; i++)
        {
            GameObject newSymbol = Instantiate(symbol);
            newSymbol.transform.SetParent(canvas.transform, false);
            canvasSymbols.Add(newSymbol);
            float positionX = 0;
            float positionY = 0;

            if (symbols == 1)
            {
                positionX = widthSpace / 2 + canvasX;
                positionY = canvasY;
            }
            else if (symbols <= 3)
            {
                float margin = Mathf.Max((widthSpace - width * symbols - width / 2) / (symbols + 1), minMargin);
                positionX = canvasX + answerWidth / 2 + margin * (i + 1) + width * i + width / 2;
                positionY = canvasHeight / 3 + canvasHeight / 6 + UnityEngine.Random.Range(-20, 100);
            }
            else if (symbols == 4)
            {
                float margin = Mathf.Max((widthSpace - width * 2 - width / 2) / 3, minMargin);
                positionX = canvasX + answerWidth / 2 + width / 2 + margin * ((i % 2) + 1) + width * (i % 2);
                if (i < 2)
                {
                    positionY = 2 * canvasHeight / 3 + (canvasHeight / 6) + UnityEngine.Random.Range(-50, 20);

                }
                else
                {

                    positionY = canvasHeight / 3 + (canvasHeight / 6) + UnityEngine.Random.Range(-50, 50);
                }
            }
            else if (symbols < 9)
            {
                positionX = canvasX + answerWidth / 2 + width / 2 + (i % 3) * (widthSpace / 3) + UnityEngine.Random.Range(0, 15);
                positionY = canvasHeight - height / 2 - ((i / 3) * (2 * canvasHeight / (3 * 3))) - UnityEngine.Random.Range(-15, 15);
            }

            newSymbol.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(width - 20, height - 20);
            newSymbol.transform.position = new Vector3(positionX, positionY - 20);
            newSymbol.SetActive(true);
        }
    }

    public void valuateAnswer(GameObject pressedButton)
    {
        signEqual.GetComponent<Button>().interactable = false;
        signNotEqual.GetComponent<Button>().interactable = false;
        pressed = pressedButton;
        if (movingImage != null)
        {
            movingImage.gameObject.SetActive(false);
        }
        movingImage = pressedButton.transform.GetChild(0).gameObject.GetComponent<Image>();
        if (pressedButton.name.Equals("SignEqual"))
        {
            if (answer == true)
            {
                if(answered == false)
                {
                    PlayerPrefs.SetInt("NumberOfRightAnswers", PlayerPrefs.GetInt("NumberOfRightAnswers") + 1);
                }
                goNext = 1;
                answerColorImage.color = new Color32(4, 161, 14, 91);
            }
            else if (answer == false)
            {
                if(answered == false)
                {
                    PlayerPrefs.SetInt("NumberOfWrongAnswers", PlayerPrefs.GetInt("NumberOfWrongAnswers") + 1);

                }
                answerColorImage.color = new Color32(161, 26, 4, 121);
                pressedButton.GetComponent<Button>().interactable = false;
            }
            shouldMove = 1;
        }
        else
        {
            if (answer == true)
            {
                if(answered == false)
                {
                    PlayerPrefs.SetInt("NumberOfWrongAnswers", PlayerPrefs.GetInt("NumberOfWrongAnswers") + 1);
                }
                answerColorImage.color = new Color32(161, 26, 4, 121);
                pressedButton.GetComponent<Button>().interactable = false;
            }
            else if (answer == false)
            {
                if(answered == false)
                {
                    PlayerPrefs.SetInt("NumberOfRightAnswers", PlayerPrefs.GetInt("NumberOfRightAnswers") + 1);

                }
                goNext = 1;
                answerColorImage.color = new Color32(4, 161, 14, 91);
                shouldMove = 1;
            }
            shouldMove = 1;
        }
        answered = true;
    }
}

