using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableArenaManager : Singleton<TableArenaManager>
{
    [SerializeField]
    public List<CardLocalArenaBehaviour> playerCardSelected;
    [SerializeField]
    public List<CardLocalArenaBehaviour> enemyCardSelected;

    [SerializeField] private EnemyBehaviour enemyBehaviour;

    public void AddPlayerCardLocalArenaBehaviour(CardLocalPlayerBehaviour cardLocalPlayerBehaviour)
    {
        CardLocalArenaBehaviour card = recoveryEmptyLocalPlayer();
        if (card != null)
        {
            card.addCard(cardLocalPlayerBehaviour);
            card.AssignProperties();
        }
    }

    public void AddEnemyCardLocalArenaBehaviour()
    {
        enemyBehaviour.card1.transform.position = enemyCardSelected[0].transform.position;
        enemyBehaviour.card2.transform.position = enemyCardSelected[1].transform.position;
    }

    public void DefaultEnemyCardLocalArenaBehaviour()
    {
        enemyBehaviour.Default();
    }
    public void DefaultPlayerCardLocalArenaBehaviour()
    {
        foreach (CardLocalArenaBehaviour card in playerCardSelected)
        {
            card.CardLocalPlayerBehaviour = null;
        }
    }


    public void RemovePlayerCardLocalArenaBehaviour(CardLocalPlayerBehaviour card)
    {
        TablePlayerManager.instance.RemoveCardLocalPlayerBehaviour(card);
    }

    public bool isNextStep()
    {
        foreach (CardLocalArenaBehaviour card in playerCardSelected)
        {
            if (card.CardLocalPlayerBehaviour == null)
                return false;
        }
        return true;
    }

    CardLocalArenaBehaviour recoveryEmptyLocalPlayer()
    {
        foreach (CardLocalArenaBehaviour card in playerCardSelected)
        {
            if (card.CardLocalPlayerBehaviour == null)
                return card;
        }
        return null;
    }


}
