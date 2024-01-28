using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CardAnimationBehaviour : MonoBehaviour
{
    [SerializeField]
    private float targetScaleFactor = 1.08f;
    private float currentScaleFactor = 1.0f;
    private float scaleSpeed = 5f; // Ajuste conforme necessário para controlar a velocidade de transição

    private bool isMouseOver = false;

    private Transform myTransform;
    private Vector3 originalScale;
    private int cardLocalLayerMask;

    public float TargetScaleFactor { get => targetScaleFactor; set => targetScaleFactor = value; }
    public float CurrentScaleFactor { get => currentScaleFactor; set => currentScaleFactor = value; }
    public float ScaleSpeed { get => scaleSpeed; set => scaleSpeed = value; }
    public bool IsMouseOver { get => isMouseOver; set => isMouseOver = value; }
    public Transform MyTransform { get => myTransform; set => myTransform = value; }
    public Vector3 OriginalScale { get => originalScale; set => originalScale = value; }

    void Start()
    {
        myTransform = GetComponent<Transform>();
        originalScale = myTransform.localScale;
    }
    void Update()
    {
        if (isMouseOver)
        {
            currentScaleFactor = Mathf.Lerp(currentScaleFactor, targetScaleFactor, Time.deltaTime * scaleSpeed);
        }
        else
        {
            currentScaleFactor = Mathf.Lerp(currentScaleFactor, 1.0f, Time.deltaTime * scaleSpeed);
        }

        myTransform.localScale = originalScale * currentScaleFactor;
    }
}
