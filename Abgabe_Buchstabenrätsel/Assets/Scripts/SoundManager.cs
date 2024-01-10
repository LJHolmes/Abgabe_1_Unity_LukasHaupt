using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioSource CorrectLetterSound;
    public AudioSource WrongSound;
    public AudioSource CorrectWordSound;
    public AudioSource WinSound;

    public Color SoundOn;
    public Color SoundOff;

    public Button SoundButton;

    public void ToggleSound()
    {
        CorrectLetterSound.mute = !CorrectLetterSound.mute;
        WrongSound.mute = !WrongSound.mute;

        if (CorrectLetterSound.mute)
        {
            SoundButton.image.color = SoundOff;
        }
        else
        {
            SoundButton.image.color = SoundOn;
        }
    }

    public void PlayCorrectLetterSound()
    {
        CorrectLetterSound.Play();
    }

    public void PlayWrongSound()
    {
        WrongSound.Play();
    }

    public void PlayCorrectWordSound()
    {
        CorrectWordSound.Play();
    }

    public void PlayWinLevelSound()
    {
        WinSound.Play();
    }
}
