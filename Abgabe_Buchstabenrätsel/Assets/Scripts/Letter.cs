using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Letter : MonoBehaviour, IPointerDownHandler
{
    private PuzzleManager puzzleManager;

    private TMP_Text letterValue;

    private Image image;

    public bool IsCorrect;
    public bool IsWrong;
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
        image = gameObject.GetComponent<Image>();

        InstantiateRandomLetter();
    }

    public void OnPointerDown(PointerEventData eventData) // Wenn Objekt gedrückt wurde
    {
        if (CorrectPressed)
        {
            return;
        }
        if (IsWrong)
        {
            image.color = WrongColor;
            puzzleManager.WrongLetter();
        }
        if (IsCorrect)
        {
            image.color = CorrectColor;
            puzzleManager.CorrectLetter(gameObject);
        }
    }

    public void InstantiateRandomLetter() // Random Buchstabe verteilen
    {
        if (IsWrong)
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
