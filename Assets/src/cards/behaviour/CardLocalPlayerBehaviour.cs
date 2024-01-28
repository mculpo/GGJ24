using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CardLocalPlayerBehaviour : MonoBehaviour
{
    [SerializeField]
    private Transform cardTranform;
    private Transform myTransform;
    private CardAnimationBehaviour cardAnimationBehaviour;

    private bool isSelected = false;

    public Transform CardTranform { get => cardTranform; set => cardTranform = value; }
    public bool IsSelected { get => isSelected; set => isSelected = value; }

    private void Start()
    {
        myTransform = GetComponent<Transform>();
        cardAnimationBehaviour = cardTranform.GetComponent<CardAnimationBehaviour>();
    }

    void OnMouseDown()
    {
        if (!isSelected)
        {
            SoundManager.instance.PlaySoundClickCard();
            TablePlayerManager.instance.AddCardLocalPlayerBehaviour(this);
            SelectedTrue();
            MouseOverFalse();
            CardInfoManager.instance.TurnOffInfo();
        }
    }


    void OnMouseEnter()
    {
        if (!isSelected)
        {
            MouseOverTrue();
            UICardInfoManager.instance.SetInformation(GetCard());
            CardInfoManager.instance.TurnOnInfo();
        }
    }

    void OnMouseExit()
    {
        if (!isSelected)
        {
            MouseOverFalse();
            CardInfoManager.instance.TurnOffInfo();
        }
    }

    public Card GetCard()
    {
        return cardTranform.GetComponent<CardBehaviour>().Card;
    }

    public void addCard(Transform transform)
    {
        cardTranform = transform;
        cardAnimationBehaviour = cardTranform.GetComponent<CardAnimationBehaviour>();
    }

    public void removeCard()
    {
        cardTranform = null;
        cardAnimationBehaviour = null;
    }
    public void AssignProperties()
    {
        cardTranform.position = myTransform.position;
        SelectedFalse();
    }

    void MouseOverTrue()
    {
        cardAnimationBehaviour.IsMouseOver = true;
    }
    void MouseOverFalse()
    {
        cardAnimationBehaviour.IsMouseOver = false;
    }
    private void SelectedTrue()
    {
        IsSelected = true;
    }

    private void SelectedFalse()
    {
        IsSelected = false;
    }
}
