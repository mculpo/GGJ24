using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class TablePlayerManager : Singleton<TablePlayerManager>
{
    public int capacitySelectCard = 2;
    [SerializeField]
    public List<CardLocalPlayerBehaviour> playerCardSelected;

    public void AddCardLocalPlayerBehaviour(CardLocalPlayerBehaviour cardLocalPlayerBehaviour)
    {
        if (playerCardSelected.Count < capacitySelectCard && !playerCardSelected.Contains(cardLocalPlayerBehaviour))
        {
            playerCardSelected.Add(cardLocalPlayerBehaviour);
            TableArenaManager.instance.AddPlayerCardLocalArenaBehaviour(cardLocalPlayerBehaviour);
            TableGameManager.instance.PlayerMoveCardFromHandToGame(cardLocalPlayerBehaviour.GetCard());
        }
    }

    public void RemoveCardLocalPlayerBehaviour(CardLocalPlayerBehaviour cardLocalPlayerBehaviour)
    {
        playerCardSelected.Remove(cardLocalPlayerBehaviour);
        CardLocalPlayerBehaviour card = cardLocalPlayerBehaviour;
        TableGameManager.instance.PlayerMoveCardFromGameToHand(cardLocalPlayerBehaviour.GetCard());
        card.AssignProperties();
    }

    public void DefaultAllCardLocalPlayer()
    {
        foreach( var card in playerCardSelected)
        {
            card.AssignProperties();
        }
        playerCardSelected.Clear();
    }
}
