using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CardSet", order = 1)]
public class CardSet : ScriptableObject
{
    public List<Card> cards;

    public List<Card> GetRandomCards(int quantity)
    {
        List<Card> randomCards = new List<Card>();
        List<Card> availableCards = new List<Card>(cards);

        for (int i = 0; i < quantity; i++)
        {
                int randomIndex = Random.Range(0, availableCards.Count);
                Card randomCard = availableCards[randomIndex];

                randomCards.Add(randomCard);
        }

        return randomCards;
    }
}
