using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ButtonNextStepBehaviour : MonoBehaviour
{
    private Transform myTransform;
    private ButtonAnimationBehaviour btnAnimationBehaviour;
    private void Start()
    {
        myTransform = GetComponent<Transform>();
        btnAnimationBehaviour = GetComponent<ButtonAnimationBehaviour>();
    }

    void OnMouseDown()
    {
        if(TableGameManager.instance.CurrentStep.Equals(TurnStep.SelectCardPhase) && TableArenaManager.instance.isNextStep())
        {
            StartCoroutine(TableGameManager.instance.NextStep());
        } else if (TableGameManager.instance.CurrentStep.Equals(TurnStep.ProcessCardTablePhase))
        {
            StartCoroutine(TableGameManager.instance.NextStep());
        }
        SoundManager.instance.PlaySoundClickCard();
    }

    void OnMouseEnter()
    {
        MouseOverTrue();
    }

    void OnMouseExit()
    {
        MouseOverFalse();
    }

    void MouseOverTrue()
    {
        btnAnimationBehaviour.IsMouseOver = true;
    }
    void MouseOverFalse()
    {
        btnAnimationBehaviour.IsMouseOver = false;
    }
}
