using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class CardEffectConditionManager
{
    private Dictionary< CardEffectCondition, 
                        System.Func<CardAbility,
                                    Player,
                                    Player,
                                    bool>> conditionChecks;

    public CardEffectConditionManager()
    {
        InitializeConditionChecks();
    }

    private void InitializeConditionChecks()
    {
        conditionChecks = new Dictionary<CardEffectCondition, System.Func<CardAbility, Player, Player, bool>>();

        conditionChecks[CardEffectCondition.AdditionalPoints] = AdditionalPoints;
        conditionChecks[CardEffectCondition.ContainsInGame] = ContainsInGame;
        conditionChecks[CardEffectCondition.OpponentHasInGame] = OpponentHasInGame;
        conditionChecks[CardEffectCondition.OpponentHasForEachInGame] = OpponentHasForEachInGame;
        conditionChecks[CardEffectCondition.PlayerHaveInGame] = PlayerHaveInGame;
        conditionChecks[CardEffectCondition.PlayerHaveForEachInGame] = PlayerHaveForEachInGame;
        conditionChecks[CardEffectCondition.OpponentHasPlayed] = OpponentHasPlayed;
        conditionChecks[CardEffectCondition.OpponentHasForEachPlayed] = OpponentHasForEachPlayed;
        conditionChecks[CardEffectCondition.PlayerHavePlayed] = PlayerHavePlayed;
        conditionChecks[CardEffectCondition.PlayerHaveForEachPlayed] = PlayerHaveForEachPlayed;
        conditionChecks[CardEffectCondition.PlayerAndOpponentHavePlayed] = PlayerAndOpponentHavePlayed;
        conditionChecks[CardEffectCondition.PlayerAndOpponentHaveInGame] = PlayerAndOpponentHaveInGame;
        conditionChecks[CardEffectCondition.OpponentHasMorePoints] = OpponentHasMorePoints;
        conditionChecks[CardEffectCondition.PlayerHaveMorePoints] = PlayerHaveMorePoints;
        conditionChecks[CardEffectCondition.SwapPointsInTurn] = SwapPointsInTurn;
        conditionChecks[CardEffectCondition.SwapPointsInAllRound] = SwapPointsInAllRound;

        conditionChecks[CardEffectCondition.OpponentHasInGameDifferentCards] = OpponentHasInGameDifferentCards;
        conditionChecks[CardEffectCondition.PlayerHaveInGameDifferentCards] = PlayerHaveInGameDifferentCards;
        conditionChecks[CardEffectCondition.AllInGameDifferentCards] = AllInGameDifferentCards;
    }

    public void DoCondition(CardEffectCondition condition, CardAbility cardAbility, Player player, Player opponent)
    {
        if (conditionChecks.ContainsKey(condition))
        {
            conditionChecks[condition](cardAbility, player, opponent);
        }
    }

    Func<CardAbility, Player, Player, bool> AdditionalPoints = (cardAbility, player, opponent) => {
        ApplyEffects(cardAbility.cardAbilityEffects, player, opponent);
        return true;
    };

    Func<CardAbility, Player, Player, bool> ContainsInGame = (cardAbility, player, opponent) => {
        foreach (var effect in cardAbility.cardAbilityEffects)
        {
            if (ContainsEffect(effect, player.CardsInGame) || ContainsEffect(effect, opponent.CardsInGame))
            {
                ApplyEffects(cardAbility.cardAbilityEffects, player, opponent);
                return true;
            }
        }
        return false;
    };

    Func<CardAbility, Player, Player, bool> OpponentHasInGame = (cardAbility, player, opponent) => {
        foreach (var effect in cardAbility.cardAbilityEffects)
        {
            if (ContainsEffect(effect, opponent.CardsInGame))
            {
                ApplyEffects(cardAbility.cardAbilityEffects, player, opponent);
                return true;
            }
        }
        return false;
    };

    Func<CardAbility, Player, Player, bool> OpponentHasForEachInGame = (cardAbility, player, opponent) => {
        int count = CountEffects(cardAbility.cardAbilityEffects, opponent.CardsInGame);

        if (count > 0)
        {
            ApplyEffects(cardAbility.cardAbilityEffects, player, opponent, count);
            return true;
        }

        return false;
    };

    Func<CardAbility, Player, Player, bool> PlayerHaveInGame = (cardAbility, player, opponent) => {
        foreach (var effect in cardAbility.cardAbilityEffects)
        {
            if (ContainsEffect(effect, player.CardsInGame))
            {
                ApplyEffects(cardAbility.cardAbilityEffects, player, opponent);
                return true;
            }
        }
        return false;
    };

    Func<CardAbility, Player, Player, bool> PlayerHaveForEachInGame = (cardAbility, player, opponent) => {
        int count = CountEffects(cardAbility.cardAbilityEffects, player.CardsInGame);

        if (count > 0)
        {
            ApplyEffects(cardAbility.cardAbilityEffects, player, opponent, count);
            return true;
        }

        return false;
    };

    Func<CardAbility, Player, Player, bool> OpponentHasPlayed = (cardAbility, player, opponent) => {
        foreach (var effect in cardAbility.cardAbilityEffects)
        {
            if (ContainsEffect(effect, opponent.CardsPlayed))
            {
                ApplyEffects(cardAbility.cardAbilityEffects, player, opponent);
                return true;
            }
        }
        return false;
    };

    Func<CardAbility, Player, Player, bool> OpponentHasForEachPlayed = (cardAbility, player, opponent) => {
        int count = CountEffects(cardAbility.cardAbilityEffects, opponent.CardsInGame);
        count += CountEffects(cardAbility.cardAbilityEffects, opponent.CardsPlayed);

        if (count > 0)
        {
            ApplyEffects(cardAbility.cardAbilityEffects, player, opponent, count);
            return true;
        }

        return false;
    };

    Func<CardAbility, Player, Player, bool> PlayerHavePlayed = (cardAbility, player, opponent) => {
        foreach (var effect in cardAbility.cardAbilityEffects)
        {
            if (ContainsEffect(effect, player.CardsPlayed))
            {
                ApplyEffects(cardAbility.cardAbilityEffects, player, opponent);
                return true;
            }
        }
        return false;
    };

    Func<CardAbility, Player, Player, bool> PlayerHaveForEachPlayed = (cardAbility, player, opponent) => {
        int count = CountEffects(cardAbility.cardAbilityEffects, player.CardsInGame);
        count += CountEffects(cardAbility.cardAbilityEffects, player.CardsPlayed);

        if (count > 0)
        {
            ApplyEffects(cardAbility.cardAbilityEffects, player, opponent, count);
            return true;
        }

        return false;
    };

    Func<CardAbility, Player, Player, bool> PlayerAndOpponentHavePlayed = (cardAbility, player, opponent) => {
        foreach (var effect in cardAbility.cardAbilityEffects)
        {
            if (ContainsEffect(effect, player.CardsPlayed) || ContainsEffect(effect, opponent.CardsPlayed))
            {
                ApplyEffects(cardAbility.cardAbilityEffects, player, opponent);
                return true;
            }
        }
        return false;
    };

    Func<CardAbility, Player, Player, bool> PlayerAndOpponentHaveInGame = (cardAbility, player, opponent) => {
        foreach (var effect in cardAbility.cardAbilityEffects)
        {
            if (ContainsEffect(effect, player.CardsInGame) || ContainsEffect(effect, opponent.CardsInGame))
            {
                ApplyEffects(cardAbility.cardAbilityEffects, player, opponent);
                return true;
            }
        }
        return false;
    };

    Func<CardAbility, Player, Player, bool> OpponentHasMorePoints = (cardAbility, player, opponent) => {
        if (!IsPlayerPointsGreaterThanOpponent(player, opponent))
            ApplyEffects(cardAbility.cardAbilityEffects, player, opponent);
        return true;
    };

    Func<CardAbility, Player, Player, bool> PlayerHaveMorePoints = (cardAbility, player, opponent) => {
        if(IsPlayerPointsGreaterThanOpponent(player, opponent))
            ApplyEffects(cardAbility.cardAbilityEffects, player, opponent);
        return true;
    };

    Func<CardAbility, Player, Player, bool> SwapPointsInTurn = (cardAbility, player, opponent) => {
        
        int currentPointPlayer = player.TotalLaughterPointsInTurn;

        player.TotalLaughterPointsInTurn = opponent.TotalLaughterPointsInTurn;
        opponent.TotalLaughterPointsInTurn = currentPointPlayer;

        ApplyEffects(cardAbility.cardAbilityEffects, player, opponent);

        return true;
    };

    Func<CardAbility, Player, Player, bool> SwapPointsInAllRound = (cardAbility, player, opponent) => {

        int currentPointPlayer = player.TotalLaughterPoints;

        player.TotalLaughterPoints = opponent.TotalLaughterPoints;
        opponent.TotalLaughterPoints = currentPointPlayer;

        ApplyEffects(cardAbility.cardAbilityEffects, player, opponent);

        return true;
    };

    Func<CardAbility, Player, Player, bool> OpponentHasInGameDifferentCards = (cardAbility, player, opponent) => {
        if (ContainsSameTypeInGame(opponent.CardsInGame))
            ApplyEffects(cardAbility.cardAbilityEffects, player, opponent);

        return true;
    };

    Func<CardAbility, Player, Player, bool> PlayerHaveInGameDifferentCards = (cardAbility, player, opponent) => {
        if (ContainsSameTypeInGame(player.CardsInGame))
            ApplyEffects(cardAbility.cardAbilityEffects, player, opponent);

        return true;
    };

    Func<CardAbility, Player, Player, bool> AllInGameDifferentCards = (cardAbility, player, opponent) => {
        if (ContainsSameTypeInGame(player.CardsInGame) || 
            ContainsSameTypeInGame(opponent.CardsInGame) || 
            ContainsSameTypeInBoth(player.CardsInGame, opponent.CardsInGame))
        {
            ApplyEffects(cardAbility.cardAbilityEffects, player, opponent);
            return true;
        }

        return true;
    };

    static bool ContainsEffect(CardAbilityEffect effect, IEnumerable<Card> cards)
    {
        return cards.Any(card => card.cardType.Equals(effect.cardTypeEffect));
    }

    static int CountEffects(IEnumerable<CardAbilityEffect> effects, List<Card> cards)
    {
        int count = 0;

        foreach (var effect in effects)
        {
            count += cards.Count(card => card.cardType.Equals(effect.cardTypeEffect));
        }

        return count;
    }

    static bool ContainsSameTypeInGame(List<Card> cardsInGame)
    {
        for (int i = 0; i < cardsInGame.Count; i++)
        {
            for (int j = i + 1; j < cardsInGame.Count; j++)
            {
                if (cardsInGame[i].cardType.Equals(cardsInGame[j].cardType))
                {
                    return true;
                }
            }
        }

        return false;
    }

    static bool ContainsSameTypeInBoth(List<Card> cards1, List<Card> cards2)
    {
        foreach (var card1 in cards1)
        {
            foreach (var card2 in cards2)
            {
                if (card1.cardType.Equals(card2.cardType))
                {
                    return true;
                }
            }
        }

        return false;
    }
    private static bool IsPlayerPointsGreaterThanOpponent(Player player, Player opponent)
    {
        return player.TotalLaughterPointsPlayerAndTurn() > opponent.TotalLaughterPointsPlayerAndTurn();
    }

    static void ApplyEffects(List<CardAbilityEffect> effects, Player player, Player opponent)
    {
        foreach (var effect in effects)
        {
            player.TotalLaughterPointsInTurn += effect.laughterPointsPositive;
            opponent.TotalLaughterPointsInTurn -= effect.laughterPointsNegative;
        }
    }

    static void ApplyEffects(List<CardAbilityEffect> effects, Player player, Player opponent, int count)
    {
        for (int i = 0; i < count; i++)
        {
            foreach (var effect in effects)
            {
                player.TotalLaughterPointsInTurn += effect.laughterPointsPositive;
                opponent.TotalLaughterPointsInTurn -= effect.laughterPointsNegative;
            }
        }
    }
}
