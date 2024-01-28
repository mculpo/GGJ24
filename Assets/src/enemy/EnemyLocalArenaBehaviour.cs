using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLocalArenaBehaviour : MonoBehaviour
{
    public CardBehaviour cardBehaviour;
    void OnMouseEnter()
    {
        if (TableGameManager.instance.CurrentStep.Equals(TurnStep.ProcessCardTablePhase))
        {
            UICardInfoManager.instance.SetInformation(cardBehaviour.Card);
            CardInfoManager.instance.TurnOnInfo();
        }
    }

    void OnMouseExit()
    {
        if (TableGameManager.instance.CurrentStep.Equals(TurnStep.ProcessCardTablePhase))
        {
            CardInfoManager.instance.TurnOffInfo();
        }
    }
}
