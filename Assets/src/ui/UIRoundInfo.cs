using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIRoundInfo : Singleton<UIRoundInfo>
{
    public TextMeshPro info;
    public void SetInfo(string value)
    {
        info.text = value;
    }
}
