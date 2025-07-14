using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public int cardId;
    public Sprite frontSprite;
    public Sprite backSprite;

    private bool isRevealed = false;
    private Image image;
    private Button button;
    private GameManager gameManager;

    void Awake()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
        gameManager = FindObjectOfType<GameManager>();
        button.onClick.AddListener(OnClick);
    }

    public void Setup(Sprite front, int id)
    {
        frontSprite = front;
        cardId = id;
        Hide();
    }

    public void OnClick()
    {
        if (!isRevealed)
        {
            Reveal();
            gameManager.CardRevealed(this);
        }
    }

    public void Reveal()
    {
        isRevealed = true;
        image.sprite = frontSprite;
    }

    public void Hide()
    {
        isRevealed = false;
        image.sprite = backSprite;
    }

    public bool IsRevealed() => isRevealed;
}
