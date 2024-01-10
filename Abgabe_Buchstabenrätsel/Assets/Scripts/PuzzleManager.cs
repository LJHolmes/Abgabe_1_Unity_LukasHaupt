using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PuzzleManager : MonoBehaviour
{
    private SoundManager soundManager;

    private float startTime;
    private float elapsedTime;

    private int RightLetterCount;
    private int RightWordsCount;

    public List<GameObject> letterList;

    public TMP_Text timerText;

    public Color CorrectColor;
    public Color WrongColor;
    public Color BaseColor;

    private void Start()
    {
        soundManager = GameObject.Find("Main Camera").GetComponent<SoundManager>();

        startTime = Time.time;

        FindLetters();
    }

    private void Update()
    {
        elapsedTime = Time.time - startTime;

        UpdateTimerText();
    }

    private void UpdateTimerText()
    {
        string formattedTime = elapsedTime.ToString("F2");

        timerText.text = formattedTime;
    }

    private void FindLetters()
    {
        foreach (GameObject letters in GameObject.FindGameObjectsWithTag("Letters"))
        {
            letterList.Add(letters);
        }
    }

    public void WrongLetter()
    {
        soundManager.PlayWrongSound();

        RightLetterCount = 0;

        Invoke("ChangeColorDelay", 0.5f);
    }

    private void ChangeColorDelay()
    {
        foreach (GameObject letter in letterList)
        {
            letter.GetComponent<Image>().color = BaseColor;
        }
    }

    public void CorrectLetter()
    {
        soundManager.PlayCorrectSound();

        RightLetterCount++;

        if (RightLetterCount == 4)
        {
            RightWordsCount++;
        }

        if (RightWordsCount == 3)
        {
            Win();
        }
    }

    private void Win()
    {

    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
