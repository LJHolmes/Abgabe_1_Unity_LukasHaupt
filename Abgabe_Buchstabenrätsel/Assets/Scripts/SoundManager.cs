using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioSource CorrectSound;
    public AudioSource WrongSound;

    public Color SoundOn;
    public Color SoundOff;

    public Button SoundButton;

    public void ToggleSound()
    {
        CorrectSound.mute = !CorrectSound.mute;
        WrongSound.mute = !WrongSound.mute;

        if (CorrectSound.mute)
        {
            SoundButton.image.color = SoundOff;
        }
        else
        {
            SoundButton.image.color = SoundOn;
        }
    }

    public void PlayCorrectSound()
    {
        CorrectSound.Play();
    }

    public void PlayWrongSound()
    {
        WrongSound.Play();
    }
}
