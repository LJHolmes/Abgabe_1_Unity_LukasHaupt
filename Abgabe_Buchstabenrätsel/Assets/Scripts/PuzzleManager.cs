using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PuzzleManager : MonoBehaviour
{
    private SoundManager soundManager;

    private GameObject winScreenPanel;

    private float startTime;
    [SerializeField] private float elapsedTime;
    [SerializeField] private float winTime;
    [SerializeField] private float highScore = 10000;

    [SerializeField] private int corrrectWordsCount;

    private int correctLetterCountWordOne;
    private int wordOneLetterCount;
    private int correctLetterCountWordTwo;
    private int wordTwoLetterCount;
    private int correctLetterCountWordThree;
    private int wordThreeLetterCount;

    private int wordsToFind = 3;

    private bool isFinishWordOne = false;
    private bool isFinishWordTwo = false;
    private bool isFinishWordThree = false;

    [SerializeField] private bool isWon = false;

    public List<GameObject> LetterList;

    public TMP_Text TimerText;
    public TMP_Text WordsCount;
    public TMP_Text HighScore;

    public Color CorrectColor;
    public Color WrongColor;
    public Color BaseColor;

    private void Start()
    {
        startTime = Time.time;

        soundManager = GameObject.Find("Main Camera").GetComponent<SoundManager>();
        winScreenPanel = GameObject.Find("WinScreen").transform.GetChild(0).gameObject;

        FindLettersAddList();

        highScore = PlayerPrefs.GetFloat("HighScore");
    }

    private void Update()
    {
        elapsedTime = Time.time - startTime;

        UpdateTimerText();
        UpdateCorrectWordsCount();
    }

    public void WrongLetter()
    {
        soundManager.PlayWrongSound();

        // Alle Werte zurücksetzen
        ResetStats();

        // Nach 0.5f Sekunden Farbe anpassen
        Invoke("ChangeColorDelay", 0.5f);
    }

    public void CorrectLetter(GameObject Letter)
    {
        // Wenn Buchstabe vom Wort 1 ausgewählt wurde
        if (Letter.GetComponent<Letter>().WordOne)
        {
            if (isFinishWordOne)
            {
                return;
            }

            if (correctLetterCountWordTwo > 0 || correctLetterCountWordThree > 0)
            {
                WrongLetter();
                return;
            }

            soundManager.PlayCorrectLetterSound();
            correctLetterCountWordOne++;

            Letter.GetComponent<Letter>().CorrectPressed = true;

            if (correctLetterCountWordOne == wordOneLetterCount)
            {
                soundManager.PlayCorrectWordSound();
                corrrectWordsCount++;
                correctLetterCountWordOne = 0;
                isFinishWordOne = true;
            }
        }

        // Wenn Buchstabe vom Wort 2 ausgewählt wurde
        if (Letter.GetComponent<Letter>().WordTwo)
        {
            if (isFinishWordTwo)
            {
                return;
            }

            if (correctLetterCountWordOne > 0 || correctLetterCountWordThree > 0)
            {
                WrongLetter();
                return;
            }

            soundManager.PlayCorrectLetterSound();
            correctLetterCountWordTwo++;

            Letter.GetComponent<Letter>().CorrectPressed = true;

            if (correctLetterCountWordTwo == wordTwoLetterCount)
            {
                soundManager.PlayCorrectWordSound();
                corrrectWordsCount++;
                correctLetterCountWordTwo = 0;
                isFinishWordTwo = true;
            }
        }

        // Wenn Buchstabe vom Wort 3 ausgewählt wurde
        if (Letter.GetComponent<Letter>().WordThree)
        {
            if (isFinishWordThree)
            {
                return;
            }

            if (correctLetterCountWordOne > 0 || correctLetterCountWordTwo > 0)
            {
                WrongLetter();
                return;
            }

            soundManager.PlayCorrectLetterSound();
            correctLetterCountWordThree++;

            Letter.GetComponent<Letter>().CorrectPressed = true;

            if (correctLetterCountWordThree == wordThreeLetterCount)
            {
                soundManager.PlayCorrectWordSound();
                corrrectWordsCount++;
                correctLetterCountWordThree = 0;
                isFinishWordThree = true;
            }
        }

        if (corrrectWordsCount == wordsToFind)
        {
            Win();
        }
    }

    private void ResetStats()
    {
        corrrectWordsCount = 0;
        correctLetterCountWordOne = 0;
        correctLetterCountWordTwo = 0;
        correctLetterCountWordThree = 0;

        isFinishWordOne = false;
        isFinishWordTwo = false;
        isFinishWordThree = false;

        foreach (GameObject letter in GameObject.FindGameObjectsWithTag("Letters"))
        {
            letter.GetComponent<Letter>().CorrectPressed = false;
        }
    }

    private void UpdateTimerText()
    {
        if (!isWon)
        {
            string formattedTime = elapsedTime.ToString("F2");

            TimerText.text = formattedTime;
        }
    }

    private void UpdateCorrectWordsCount()
    {
        WordsCount.text = corrrectWordsCount.ToString();
    }

    private void FindLettersAddList()
    {
        foreach (GameObject letter in GameObject.FindGameObjectsWithTag("Letters"))
        {
            if (letter.GetComponent<Letter>().WordOne)
            {
                wordOneLetterCount++;
            }
            if (letter.GetComponent<Letter>().WordTwo)
            {
                wordTwoLetterCount++;
            }
            if (letter.GetComponent<Letter>().WordThree)
            {
                wordThreeLetterCount++;
            }

            LetterList.Add(letter);
        }
    }

    private void ChangeColorDelay()
    {
        foreach (GameObject letter in LetterList)
        {
            letter.GetComponent<Image>().color = BaseColor;
        }
    }

    private void Win()
    {
        soundManager.PlayWinLevelSound();

        isWon = true;

        winTime = elapsedTime;

        winScreenPanel.SetActive(true);

        if (winTime < highScore)
        {
            PlayerPrefs.SetFloat("HighScore", winTime);
            HighScore.text = winTime.ToString();
        }
        else
        {
            HighScore.text = highScore.ToString();
        }
    }

    public void ResetLettersRandom()
    {
        foreach (GameObject letter in LetterList)
        {
            if (letter.GetComponent<Letter>().isWrong)
            {
                letter.GetComponent<Letter>().InstantiateRandomLetter();
            }
        }
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
