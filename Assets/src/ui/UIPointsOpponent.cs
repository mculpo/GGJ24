using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIPointsOpponent : Singleton<UIPointsOpponent>
{
    public TextMeshPro points;
    public float maxScale = 0.5f;
    public float duration = 0.2f;

    private Vector3 initialScale;

    void Start()
    {
        initialScale = transform.localScale;
    }

    public void SetPoints(int value)
    {
        points.text = value.ToString();
        StartCoroutine(ScaleUpEffectCoroutine());
    }

    IEnumerator ScaleUpEffectCoroutine()
    {
        // Upscale
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float progress = elapsedTime / duration;
            transform.localScale = Vector3.Lerp(initialScale, initialScale * maxScale, progress);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // Ensure the final scale is exactly the maximum scale
        transform.localScale = initialScale * maxScale;


        // Reset to the original scale
        elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float progress = elapsedTime / duration;
            transform.localScale = Vector3.Lerp(initialScale * maxScale, initialScale, progress);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // Ensure the final scale is exactly the initial scale
        transform.localScale = initialScale;
    }
}
