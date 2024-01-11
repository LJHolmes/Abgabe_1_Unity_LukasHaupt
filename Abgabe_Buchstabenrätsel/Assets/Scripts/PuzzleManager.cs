using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Analytics;
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

    [SerializeField] private int corrrectWordsCount = 0;

    private int correctLetterCountWordOne;
    private int wordOneLetterCount;
    private int correctLetterCountWordTwo;
    private int wordTwoLetterCount;
    private int correctLetterCountWordThree;
    private int wordThreeLetterCount;

    private int wordsToFind = 3;

    private bool finishWordOne = false;
    private bool finishWordTwo = false;
    private bool finishWordThree = false;

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
        soundManager = GameObject.Find("Main Camera").GetComponent<SoundManager>();
        winScreenPanel = GameObject.Find("WinScreen").transform.GetChild(0).gameObject;

        startTime = Time.time;

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

        ResetStats();

        Invoke("ChangeColorDelay", 0.5f);
    }

    public void CorrectLetter(GameObject Letter)
    {
        if (Letter.GetComponent<Letter>().WordOne)
        {
            if (finishWordOne)
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
                finishWordOne = true;
            }
        }

        if (Letter.GetComponent<Letter>().WordTwo)
        {
            if (finishWordTwo)
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
                finishWordTwo = true;
            }
        }

        if (Letter.GetComponent<Letter>().WordThree)
        {
            if (finishWordThree)
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
                finishWordThree = true;
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

        finishWordOne = false;
        finishWordTwo = false;
        finishWordThree = false;

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
        foreach (GameObject letters in GameObject.FindGameObjectsWithTag("Letters"))
        {
            if (letters.GetComponent<Letter>().WordOne)
            {
                wordOneLetterCount++;
            }
            if (letters.GetComponent<Letter>().WordTwo)
            {
                wordTwoLetterCount++;
            }
            if (letters.GetComponent<Letter>().WordThree)
            {
                wordThreeLetterCount++;
            }

            LetterList.Add(letters);
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

    public void ResetLetters()
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
