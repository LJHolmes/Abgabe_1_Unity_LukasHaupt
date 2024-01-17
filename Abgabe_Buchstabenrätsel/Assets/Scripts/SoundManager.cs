using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    private Button soundButton;

    public AudioSource CorrectLetterSound;
    public AudioSource WrongSound;
    public AudioSource CorrectWordSound;
    public AudioSource WinSound;

    public Color SoundOn;
    public Color SoundOff;

    private void Start()
    {
        soundButton = GameObject.Find("SoundButton").GetComponent<Button>();
    }

    public void ToggleSound()
    {
        CorrectLetterSound.mute = !CorrectLetterSound.mute;
        WrongSound.mute = !WrongSound.mute;
        CorrectWordSound.mute = !CorrectWordSound.mute;
        WinSound.mute = !WinSound.mute;

        if (CorrectLetterSound.mute)
        {
            soundButton.image.color = SoundOff;
        }
        else
        {
            soundButton.image.color = SoundOn;
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
