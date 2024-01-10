using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Letter : MonoBehaviour
{
    private SoundManager soundManager;

    private TMP_Text letterValue;

    void Start()
    {
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    public void CorrectLetter()
    {
        soundManager.PlayCorrectSound();
    }

    public void WrongLetter()
    {
        soundManager.PlayWrongSound();
    }
}
