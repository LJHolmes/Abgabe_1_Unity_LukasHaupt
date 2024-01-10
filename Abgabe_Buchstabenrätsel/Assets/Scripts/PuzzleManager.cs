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

    [SerializeField] private int correctLetterCount = 0;
    [SerializeField] private int corrrectWordsCount = 0;

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

        correctLetterCount = 0;
        corrrectWordsCount = 0;

        Invoke("ChangeColorDelay", 0.5f);
    }

    public void CorrectLetter()
    {
        soundManager.PlayCorrectLetterSound();

        correctLetterCount++;

        if (correctLetterCount == 4)
        {
            soundManager.PlayCorrectWordSound();
            corrrectWordsCount++;
            correctLetterCount = 0;
        }

        if (corrrectWordsCount == 3)
        {
            Win();
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
