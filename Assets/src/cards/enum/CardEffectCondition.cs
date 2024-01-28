using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[System.Serializable]
public enum CardEffectCondition
{
    AdditionalPoints,
    ContainsInGame,

    OpponentHasInGame,
    OpponentHasForEachInGame,
    PlayerHaveInGame,
    PlayerHaveForEachInGame,

    OpponentHasPlayed,
    OpponentHasForEachPlayed,
    PlayerHavePlayed,
    PlayerHaveForEachPlayed,

    PlayerAndOpponentHavePlayed,
    PlayerAndOpponentHaveInGame,

    OpponentHasMorePoints,
    PlayerHaveMorePoints,

    SwapPointsInTurn,
    SwapPointsInAllRound,

    CopyCardRandomly,

    OpponentHasInGameDifferentCards,
    PlayerHaveInGameDifferentCards,
    AllInGameDifferentCards
}
