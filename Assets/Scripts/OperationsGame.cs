using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OperationsGame : MonoBehaviour
{

    enum Operation
    {
        Addition,
        Substraction,
        Multiplication,
        Division
    }

    enum Type
    {
        Numbers,
        Symbols,
        Blocks
    }

    public GameObject canvas;
    public TextMeshProUGUI firstOperand, secondOperand;
    public Image firstImage, secondImage;
    public TextMeshProUGUI operationText;
    public Image answerFrame;
    public GameObject[] signs;
    public Image answerColorImage;

    private int roundsPlayed;
    private int toNumber;
    private int numberOfAnswers;
    private Operation operation;
    private Type type;
    private List<Operation> operations;

    private int leftOperand;
    private int rightOperand;
    private GameObject rightButton;

    private int shouldMove = 0;
    private List<GameObject> triedAnswers;
    private int goNext = 0;
    private GameObject pressed;
    private Vector3 destination;
    private int maxRounds;

    public Stopwatch timer;
    void Start()
    {
        timer = new Stopwatch();
        timer.Start();
        PlayerPrefs.SetInt("NumberOfRightAnswers", 0);
        PlayerPrefs.SetInt("NumberOfWrongAnswers", 0);
        maxRounds = PlayerPrefs.GetInt("NumberOfTasks");
        print("Maxrounds:" + maxRounds);
        roundsPlayed = 0;

        toNumber = PlayerPrefs.GetInt("toNumber");
        print("DO broja: " + toNumber);
        destination = answerFrame.transform.position;
        
        numberOfAnswers = Mathf.Min(toNumber, PlayerPrefs.GetInt("NumberOfAnswers"));
        
        operations = new List<Operation>();
        setAvailableOperations();

        SetTypeOfAnswer();
        
        StartGame();
    }

    private void SetTypeOfAnswer()
    {
        String typeFromPrefs = PlayerPrefs.GetString("typeOfAnswer");
        print("S:" + typeFromPrefs);
        switch (typeFromPrefs)
        {
            case "Numbers":
                type = Type.Numbers;
                break;
            case "Symbols":
                type = Type.Symbols;
                if(toNumber > 9)
                {
                    toNumber = 9;
                }
                break;
            case "Blocks":
                type = Type.Blocks;
                break;
        }
    }


    private void StartGame()
    {
        triedAnswers = new List<GameObject>();
        pressed = null;

        operation = operations[UnityEngine.Random.Range(0, operations.Count)];
        SetOperationText();
        GetRandomOperandsDependingOnOperation();

        print(type.ToString());
        switch (type)
        {
            case Type.Numbers:
                firstOperand.SetText(leftOperand.ToString());
                secondOperand.SetText(rightOperand.ToString());
                break;
            case Type.Symbols:
                while(leftOperand > 9 || rightOperand > 9) {
                    GetRandomOperandsDependingOnOperation();
                }
                Sprite leftImage = Resources.Load<Sprite>("Apples/apple" + leftOperand);
                Sprite rightImage = Resources.Load<Sprite>("Apples/apple" + rightOperand);
                firstImage.sprite = leftImage;
                secondImage.sprite = rightImage;
                firstImage.gameObject.SetActive(true);
                secondImage.gameObject.SetActive(true);
                break;
            case Type.Blocks:
                firstImage.sprite = Resources.Load<Sprite>("Trees/tree" + leftOperand);
                secondImage.sprite = Resources.Load<Sprite>("Trees/tree" + rightOperand);
                firstImage.gameObject.SetActive(true);
                secondImage.gameObject.SetActive(true);
                break;
        }
        int result = Result();
        renderSigns();
    }

    private void SetOperationText()
    {
        switch (operation)
        {
            case Operation.Addition:
                operationText.SetText("+");
                break;
            case Operation.Substraction:
                operationText.SetText("-");
                break;
            case Operation.Multiplication:
                operationText.SetText("*");
                break;
            case Operation.Division:
                operationText.SetText("/");
                break;
        }
    }

    private void renderSigns()
    {
        List<int> answers = new List<int>();

        for (int i = 0; i < numberOfAnswers - 1; i++)
        {
            int random = UnityEngine.Random.Range(0, toNumber + 1);
            while (answers.Contains(random) || random == Result())
            {
                random = UnityEngine.Random.Range(0, toNumber + 1);
            }
            answers.Add(random);
        }
        print(answers);

        int minMargin = 20;
        int minWidth = 160;
        int screenWidth = Screen.width;

        float canvasX = canvas.GetComponent<Canvas>().transform.position.x;
        float width = Mathf.Min(minWidth, (screenWidth - minMargin * (numberOfAnswers + 1)) / numberOfAnswers);
        float height = width - 25;
        float margin = Mathf.Max((screenWidth - width * numberOfAnswers) / (numberOfAnswers + 1), minMargin);

        float leftMargin = (screenWidth - numberOfAnswers * (width)) / (numberOfAnswers + 1);
        int leftBoundary = (screenWidth / 2) * (-1);

        for (int index = 0; index < numberOfAnswers; index++)
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
            button.transform.GetChild(1).gameObject.transform.position = newPosition;
        }

        int randomRightSign = UnityEngine.Random.Range(0, numberOfAnswers);
        GameObject rightSign = signs[randomRightSign];

        rightButton = signs[randomRightSign]; 
        int res = Result();

        switch (type)
        {
            case Type.Numbers:
                rightSign.GetComponentInChildren<TextMeshProUGUI>().SetText("" + res);
                break;
            case Type.Symbols:
                if(res == 0)
                {
                    rightSign.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ":)";
                    rightSign.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().gameObject.SetActive(false);
                }
                rightSign.gameObject.transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("Apples/apple" + res);
                rightSign.gameObject.transform.Find("Image").gameObject.SetActive(true);
                break;
            case Type.Blocks:
                if (res == 0)
                {
                    rightSign.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ":)";
                    rightSign.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().gameObject.SetActive(false);
                }
                rightSign.gameObject.transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("Trees/tree" + res);
                rightSign.gameObject.transform.Find("Image").gameObject.SetActive(true);
                break;
        }
        
        List<GameObject> wrongSigns = new List<GameObject>();

        for (int i = 0, k = 0; i < numberOfAnswers; i++)
        {
            if (i != randomRightSign)
            {
                wrongSigns.Add(signs[i]);
                int wrongAns = answers[k++];
                
                switch (type)
                {
                    case Type.Numbers:
                        signs[i].GetComponentInChildren<TextMeshProUGUI>().SetText("" + wrongAns);
                        break;
                    case Type.Symbols:
                        if(wrongAns == 0)
                        {
                            signs[i].gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().gameObject.SetActive(false);
                            signs[i].gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ":(";
                        }
                        signs[i].gameObject.transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("Apples/apple" + wrongAns);
                        signs[i].gameObject.transform.Find("Image").gameObject.SetActive(true);
                        break;
                    case Type.Blocks:
                        if (wrongAns == 0)
                        {
                            signs[i].gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().gameObject.SetActive(false);
                            signs[i].gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = ":(";
                        }
                        signs[i].gameObject.transform.Find("Image").GetComponent<Image>().sprite = Resources.Load<Sprite>("Trees/tree" + wrongAns);
                        signs[i].gameObject.transform.Find("Image").gameObject.SetActive(true);
                        break;
                }
                
            }
        }
    }
    private void GetRandomOperandsDependingOnOperation()
    {
        bool check = false;

        while (!check)
        {
            leftOperand = UnityEngine.Random.Range(0, toNumber) + 1;
            rightOperand = UnityEngine.Random.Range(0, toNumber) + 1;
            int result = Result();

            if(Operation.Addition == operation)
            {
                if(result <= toNumber)
                {
                    check = true;
                }
            }
            else if (Operation.Substraction == operation)
            {
                if (result >= 0 && result <= toNumber)
                {
                    check = true;
                }
            }
            else if (Operation.Division == operation)
            {
                float resFloat = (float)leftOperand / (float)rightOperand;
                int resInt = leftOperand / rightOperand;
                if (resFloat == (float)resInt)
                {
                    check = true;
                }
            }
            else
            {
                if (result <= toNumber)
                {
                    check = true;
                }
            }
        }
    }

    private void setAvailableOperations()
    {
        if(PlayerPrefs.GetInt("Addition") == 1) operations.Add(Operation.Addition);
        if (PlayerPrefs.GetInt("Substraction") == 1) operations.Add(Operation.Substraction);
        if (PlayerPrefs.GetInt("Multiplication") == 1) operations.Add(Operation.Multiplication);
        if (PlayerPrefs.GetInt("Division") == 1) operations.Add(Operation.Division);
    }

    private int Result()
    {
        switch (operation)
        {
            case Operation.Addition:
                return leftOperand + rightOperand;
            case Operation.Substraction:
                return leftOperand - rightOperand;
            case Operation.Multiplication:
                return leftOperand * rightOperand;
            case Operation.Division:
                return leftOperand / rightOperand;
        }
        return -1;
    }

    // Update is called once per frame
    void Update()
    {
        if(shouldMove == 1)
        {
            if (destination != pressed.transform.position)
            {
                IncrementPosition();
            }
            else
            {
                if (goNext == 1)
                {
                    goNext = 0;
                    shouldMove = 0;
                    roundsPlayed++;
                    if(roundsPlayed >= maxRounds && maxRounds != 0)
                    {
                        timer.Stop();
                        TimeSpan elapsed = timer.Elapsed;
                        PlayerPrefs.SetString("TotalTime", "" + elapsed.Minutes + ":" + elapsed.Seconds);
                        print("Prošlo vremena: " + timer.Elapsed);
                        PlayerPrefs.SetInt("NextScene", 4);
                        SceneManager.LoadScene(5);
                    }
                    else
                    {
                        foreach(GameObject obj in signs)
                        {
                            obj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
                        }
                        StartGame();
                    }
                }

                foreach (GameObject obj in signs)
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

    public void valuateAnswer(GameObject pressedButton)
    {
        foreach(GameObject obj in signs)
        {
            obj.GetComponent<Button>().interactable = false;
        }

        if(pressed != null)
        {
            pressed.gameObject.SetActive(false);
        }

        switch (type)
        {
            case Type.Numbers:
                pressed = pressedButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().gameObject;
                break;
            case Type.Symbols:
                if(pressedButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Equals(""))
                {
                    pressed = pressedButton.transform.GetChild(1).GetComponent<Image>().gameObject;
                } else
                {
                    pressedButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().gameObject.SetActive(true);
                    pressed = pressedButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().gameObject;
                }
                break;
            case Type.Blocks:
                if (pressedButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Equals(""))
                {
                    pressed = pressedButton.transform.GetChild(1).GetComponent<Image>().gameObject;
                }
                else
                {
                    pressedButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().gameObject.SetActive(true);
                    pressed = pressedButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().gameObject;
                }
                break;
        }

        if (pressedButton.Equals(rightButton))
        {
            if (triedAnswers.Count == 0)
            {
                PlayerPrefs.SetInt("NumberOfRightAnswers", PlayerPrefs.GetInt("NumberOfRightAnswers") + 1);
            }
            goNext = 1;
            answerColorImage.color = new Color32(4, 161, 14, 91);
            shouldMove = 1;
        } 
        else 
        {
            if (triedAnswers.Count == 0)
            {
                PlayerPrefs.SetInt("NumberOfWrongAnswers", PlayerPrefs.GetInt("NumberOfWrongAnswers") + 1);
            }
            triedAnswers.Add(pressedButton);
            answerColorImage.color = new Color32(161, 26, 4, 121);
            //pressedButton.SetActive(false);
            Image img = pressedButton.GetComponent<Image>();
            shouldMove = 1;
            pressedButton.GetComponent<Button>().interactable = false;
        }
    }

    void IncrementPosition()
    {
        // Calculate the next position
        float speed = 400;
        float delta = speed * Time.deltaTime;
        Vector3 currentPosition = pressed.transform.position;
        Vector3 nextPosition = Vector3.MoveTowards(currentPosition, destination, delta);

        // Move the object to the next position
        pressed.transform.position = nextPosition;
    }


}
