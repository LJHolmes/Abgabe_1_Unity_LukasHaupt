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

    public Color CorrectColor;
    public Color WrongColor;

    void Start()
    {
        puzzleManager = GameObject.Find("PuzzleManager").GetComponent<PuzzleManager>();
        letterValue = gameObject.transform.GetChild(0).GetComponent<TMP_Text>();

        InstantiateRandomLetter();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isWrong)
        {
            gameObject.GetComponent<Image>().color = WrongColor;
            puzzleManager.WrongLetter();
        }
        if (isCorrect)
        {
            gameObject.GetComponent<Image>().color = CorrectColor;
            puzzleManager.CorrectLetter();
        }
    }

    private void InstantiateRandomLetter()
    {
        if (isWrong)
        {
            int randomIndex = Random.Range(0, 26);

            // Converting the index to a character
            char randomLetter = (char)('A' + randomIndex);

            letterValue.text = randomLetter.ToString();
        }
        else
        {
            return;
        }
    }
}
