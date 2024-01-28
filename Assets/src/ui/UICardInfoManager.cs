using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UICardInfoManager : Singleton<UICardInfoManager>
{
    public TextMeshPro nameCard;
    public TextMeshPro typeCard;
    public TextMeshPro pointers;
    public TextMeshPro ability;

    public void SetInformation(Card card)
    {
        nameCard.text = card.cardName;
        typeCard.text = card.cardType.ToString();
        pointers.text = card.laughterPoints.ToString();
        ability.text = card.cardAbility.description;
    }
}
