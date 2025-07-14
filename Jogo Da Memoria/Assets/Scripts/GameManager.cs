using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject cardPrefab;
    public Sprite[] cardFaces;
    public Sprite cardBack;
    public Transform cardParent;

    private List<Card> cards = new List<Card>();
    private Card firstCard;
    private Card secondCard;
    private bool isBusy = false;

    void Start()
    {
        CreateCards();
    }

    void CreateCards()
    {
        List<Sprite> selectedFaces = new List<Sprite>();
        selectedFaces.AddRange(cardFaces);
        selectedFaces.AddRange(cardFaces); // duplicar para pares

        // Embaralhar
        for (int i = 0; i < selectedFaces.Count; i++)
        {
            Sprite temp = selectedFaces[i];
            int rand = Random.Range(i, selectedFaces.Count);
            selectedFaces[i] = selectedFaces[rand];
            selectedFaces[rand] = temp;
        }

        for (int i = 0; i < selectedFaces.Count; i++)
        {
            GameObject cardObj = Instantiate(cardPrefab, cardParent);
            Card card = cardObj.GetComponent<Card>();
            card.backSprite = cardBack;
            card.Setup(selectedFaces[i], i / 2);
            cards.Add(card);
        }
    }

    public void CardRevealed(Card card)
    {
        if (isBusy || card == firstCard) return;

        if (firstCard == null)
        {
            firstCard = card;
        }
        else
        {
            secondCard = card;
            StartCoroutine(CheckMatch());
        }
    }

    IEnumerator CheckMatch()
    {
        isBusy = true;
        yield return new WaitForSeconds(1f);

        if (firstCard.cardId == secondCard.cardId)
        {
            // Match — manter reveladas
        }
        else
        {
            firstCard.Hide();
            secondCard.Hide();
        }

        firstCard = null;
        secondCard = null;
        isBusy = false;
    }
}