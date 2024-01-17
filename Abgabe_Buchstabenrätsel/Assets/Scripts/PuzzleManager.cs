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
    [SerializeField] private int wordsToFind = 3;

    [SerializeField] private bool isWon = false;

    private int correctLetterCountWordOne;
    private int wordOneLetterCount;
    private int correctLetterCountWordTwo;
    private int wordTwoLetterCount;
    private int correctLetterCountWordThree;
    private int wordThreeLetterCount;

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

        FindLettersAndAddToList();

        highScore = PlayerPrefs.GetFloat("HighScore");
    }

    private void Update()
    {
        elapsedTime = Time.time - startTime;

        UpdateTimerTextUI();
        UpdateCorrectWordsCountUI();
    }

    public void WrongLetter()
    {
        soundManager.PlayWrongSound();

        ResetStats();

        Invoke("ChangeColorDelay", 0.5f);
    }

    public void CorrectLetter(GameObject letter)
    {
        var letterscript = letter.GetComponent<Letter>();

        // Wenn Buchstabe vom Wort 1 ausgewählt wurde
        if (letterscript.WordOne)
        {
            if (correctLetterCountWordTwo > 0 || correctLetterCountWordThree > 0)
            {
                WrongLetter();
                return;
            }

            soundManager.PlayCorrectLetterSound();
            correctLetterCountWordOne++;

            letterscript.CorrectPressed = true;

            if (correctLetterCountWordOne == wordOneLetterCount)
            {
                soundManager.PlayCorrectWordSound();
                corrrectWordsCount++;
                correctLetterCountWordOne = 0;
            }
        }

        // Wenn Buchstabe vom Wort 2 ausgewählt wurde
        if (letterscript.WordTwo)
        {
            if (correctLetterCountWordOne > 0 || correctLetterCountWordThree > 0)
            {
                WrongLetter();
                return;
            }

            soundManager.PlayCorrectLetterSound();
            correctLetterCountWordTwo++;

            letterscript.CorrectPressed = true;

            if (correctLetterCountWordTwo == wordTwoLetterCount)
            {
                soundManager.PlayCorrectWordSound();
                corrrectWordsCount++;
                correctLetterCountWordTwo = 0;
            }
        }

        // Wenn Buchstabe vom Wort 3 ausgewählt wurde
        if (letterscript.WordThree)
        {
            if (correctLetterCountWordOne > 0 || correctLetterCountWordTwo > 0)
            {
                WrongLetter();
                return;
            }

            soundManager.PlayCorrectLetterSound();
            correctLetterCountWordThree++;

            letterscript.CorrectPressed = true;

            if (correctLetterCountWordThree == wordThreeLetterCount)
            {
                soundManager.PlayCorrectWordSound();
                corrrectWordsCount++;
                correctLetterCountWordThree = 0;
            }
        }

        if (corrrectWordsCount == wordsToFind)
        {
            Win();
        }
    }

    private void ResetStats() // Werte zurücksetzen
    {
        corrrectWordsCount = 0;
        correctLetterCountWordOne = 0;
        correctLetterCountWordTwo = 0;
        correctLetterCountWordThree = 0;

        foreach (GameObject letter in GameObject.FindGameObjectsWithTag("Letters"))
        {
            letter.GetComponent<Letter>().CorrectPressed = false;
        }
    }

    private void UpdateTimerTextUI()
    {
        if (!isWon)
        {
            string formattedTime = elapsedTime.ToString("F2");

            TimerText.text = formattedTime;
        }
    }

    private void UpdateCorrectWordsCountUI()
    {
        WordsCount.text = corrrectWordsCount.ToString();
    }

    private void FindLettersAndAddToList() // Letters in Liste hinzufügen und Wortlänge anpassen
    {
        foreach (GameObject letter in GameObject.FindGameObjectsWithTag("Letters"))
        {
            var letterscript = letter.GetComponent<Letter>();

            if (letterscript.WordOne)
            {
                wordOneLetterCount++;
            }
            if (letterscript.WordTwo)
            {
                wordTwoLetterCount++;
            }
            if (letterscript.WordThree)
            {
                wordThreeLetterCount++;
            }

            LetterList.Add(letter);
        }
    }

    private void ChangeColorDelay() // Farbe wird nach 0.5f Sekunden zurückgesetzt
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

        CheckHighScore();
    }

    private void CheckHighScore()
    {
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
