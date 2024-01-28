using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardLocalArenaBehaviour : MonoBehaviour
{
    [SerializeField]
    private CardLocalPlayerBehaviour cardLocalPlayerBehaviour;
    private Transform myTransform;

    public CardLocalPlayerBehaviour CardLocalPlayerBehaviour { get => cardLocalPlayerBehaviour; set => cardLocalPlayerBehaviour = value; }

    private void Start()
    {
        myTransform = GetComponent<Transform>();    
    }

    // Start is called before the first frame update
    void OnMouseDown()
    {
        if (cardLocalPlayerBehaviour != null)
        {
            SoundManager.instance.PlaySoundClickCard();
            TableArenaManager.instance.RemovePlayerCardLocalArenaBehaviour(cardLocalPlayerBehaviour);
            CardInfoManager.instance.TurnOffInfo();
            cardLocalPlayerBehaviour = null;
        }
    }

    void OnMouseEnter()
    {
        if (cardLocalPlayerBehaviour != null)
        {
            UICardInfoManager.instance.SetInformation(cardLocalPlayerBehaviour.GetCard());
            CardInfoManager.instance.TurnOnInfo();
        }
    }

    void OnMouseExit()
    {
        if (cardLocalPlayerBehaviour != null)
        {
            CardInfoManager.instance.TurnOffInfo();
        }
    }

    public void addCard(CardLocalPlayerBehaviour cardLocalPlayerBehaviour)
    {
        this.cardLocalPlayerBehaviour = cardLocalPlayerBehaviour;
    }

    public void Clean()
    {
        cardLocalPlayerBehaviour = null;
    }

    public void AssignProperties()
    {
        cardLocalPlayerBehaviour.CardTranform.position = myTransform.position;
    }
}
