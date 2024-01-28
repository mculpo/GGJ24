using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBehaviour : MonoBehaviour
{
    [SerializeField]
    private Card card;

    public Card Card { get => card; set => card = value; }

    public void addCard(Card card)
    {
        Card = card;
    }
}
