using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public TMP_Text timerText;

    private float startTime;
    private float elapsedTime;

    void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {
        elapsedTime = Time.time - startTime;

        UpdateTimerText();
    }

    void UpdateTimerText()
    {
        string formattedTime = elapsedTime.ToString("F2");

        timerText.text = formattedTime;
    }
}
