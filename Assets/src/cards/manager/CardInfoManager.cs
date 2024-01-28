using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardInfoManager : Singleton<CardInfoManager>
{
    [SerializeField]
    private GameObject infoCard;

    public void TurnOnInfo()
    {
        infoCard.SetActive(true);
    }

    public void TurnOffInfo()
    {
        infoCard.SetActive(false);
    }
}
