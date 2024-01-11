using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Letter : MonoBehaviour, IPointerDownHandler
{
    private PuzzleManager puzzleManager;

    private TMP_Text letterValue;

    public bool isCorrect;
    public bool isWrong;
    public bool WordOne;
    public bool WordTwo;
    public bool WordThree;

    public bool CorrectPressed;

    public Color CorrectColor;
    public Color WrongColor;

    void Start()
    {
        puzzleManager = GameObject.Find("PuzzleManager").GetComponent<PuzzleManager>();
        letterValue = gameObject.transform.GetChild(0).GetComponent<TMP_Text>();

        // Random Buchstabe verteilen
        InstantiateRandomLetter();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (CorrectPressed)
        {
            return;
        }
        if (isWrong)
        {
            gameObject.GetComponent<Image>().color = WrongColor;
            puzzleManager.WrongLetter();
        }
        if (isCorrect)
        {
            gameObject.GetComponent<Image>().color = CorrectColor;
            puzzleManager.CorrectLetter(gameObject);
        }
    }

    public void InstantiateRandomLetter()
    {
        if (isWrong)
        {
            int randomIndex = Random.Range(0, 26);

            // Int zu Buchstaben
            char randomLetter = (char)('A' + randomIndex);

            letterValue.text = randomLetter.ToString();
        }
        else
        {
            return;
        }
    }
}
