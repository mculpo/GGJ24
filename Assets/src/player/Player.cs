using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Player
{
    private int totalCardInHand = 4;

    [SerializeField]
    private int totalLaughterPointsInTurn;
    [SerializeField]
    private int totalLaughterPoints;
    [SerializeField]
    private List<Card> cardsInGame;
    [SerializeField]
    private List<Card> cardsPlayed;
    [SerializeField]
    private List<Card> cardsInDeck;
    [SerializeField]
    private List<Card> cardsInHand;


    public int TotalLaughterPoints { get => totalLaughterPoints; set => totalLaughterPoints = value; }
    public List<Card> CardsInGame { get => cardsInGame; set => cardsInGame = value; }
    public List<Card> CardsPlayed { get => cardsPlayed; set => cardsPlayed = value; }
    public List<Card> CardsInDeck { get => cardsInDeck; set => cardsInDeck = value; }
    public List<Card> CardsInHand { get => cardsInHand; set => cardsInHand = value; }
    public int TotalLaughterPointsInTurn { get => totalLaughterPointsInTurn; set => totalLaughterPointsInTurn = value; }

    public void UpdateCurrentLaughterPointer()
    {
        TotalLaughterPoints += TotalLaughterPointsInTurn;
        TotalLaughterPointsInTurn = 0;
    }

    public void addLaughterPoints(int value)
    {
        totalLaughterPoints += value;
    }

    public void removeLaughterPoints(int value)
    {
        totalLaughterPoints -= value;
        validateTotalLaughterPointsMin();
    }

    public void AssignPointsCardToCurrentPointTurn()
    {
        CardsInGame.ForEach(card =>
        {
            TotalLaughterPointsInTurn += card.laughterPoints;
        });
    }

    public void MoveCardsFromDeckToHand()
    {
        int numberOfCardsToMove = MissingCardHand();
        if (cardsInDeck.Count >= numberOfCardsToMove)
        {
            List<Card> cardsToMove = cardsInDeck.GetRange(0, numberOfCardsToMove);
            cardsInDeck.RemoveRange(0, numberOfCardsToMove);
            cardsInHand.AddRange(cardsToMove);
        }
    }

    public void MoveCardsFromInGameToPlayed()
    {
        if (CardsInGame.Count > 0)
        {
            CardsPlayed.AddRange(CardsInGame);
            CardsInGame.Clear();
        }
    }

    public void MoveCardFromHandToGame(Card card)
    {
        if (CardsInHand.Contains(card))
        {
            CardsInHand.Remove(card);
            CardsInGame.Add(card);
        }
        else
        {
            Debug.LogWarning("Card não encontrado em CardsInHand.");
        }
    }

    public void MoveCardFromGameToHand(Card card)
    {
        if (CardsInGame.Contains(card))
        {
            CardsInGame.Remove(card);
            CardsInHand.Add(card);
        }
        else
        {
            Debug.LogWarning("Card não encontrado em CardsInGame.");
        }
    }

    public void MoveCardsFromDeckToHandIA(int numberOfCardsToMove)
    {
        if (numberOfCardsToMove > 0 && numberOfCardsToMove <= CardsInHand.Count)
        {
            for (int i = 0; i < numberOfCardsToMove; i++)
            {
                CardsInGame.Add(CardsInHand[i]);
            }

            CardsInHand.RemoveRange(0, numberOfCardsToMove);
        }
    }

    public int TotalLaughterPointsPlayerAndTurn()
    {
        return TotalLaughterPoints + TotalLaughterPointsInTurn;
    }

    private int MissingCardHand()
    {
        return totalCardInHand - CardsInHand.Count;
    }

    private void validateTotalLaughterPointsMin()
    {
        if (totalLaughterPoints < 0)
        {
            totalLaughterPoints = 0;
        }
    }
    private void validateTotalLaughterPointsMax()
    {
        if (totalLaughterPoints >= 100)
        {
            totalLaughterPoints = 100;
        }
    }
}
