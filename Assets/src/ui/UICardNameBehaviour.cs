using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UICardNameBehaviour : MonoBehaviour
{
    public TextMeshPro cardName;

    public void setCardName(string name)
    {
        cardName.text = name;
    }
}
