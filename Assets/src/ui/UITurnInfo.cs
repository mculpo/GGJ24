using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UITurnInfo : Singleton<UITurnInfo>
{
    public TextMeshPro turn;

    public void SetTurn(int value)
    {
        if(value >= 3)
        {
            turn.text = "Final Turn";
        } 
        else
        {
            turn.text = "Turn: " + value.ToString();
        }
        
    }
}
