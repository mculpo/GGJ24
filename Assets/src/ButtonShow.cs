using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonShow : MonoBehaviour
{
    public GameObject objectShow;
    public bool active;

    private void OnMouseDown()
    {
        SoundManager.instance.PlaySoundClickCard();
        StartCoroutine(activite());
    }

    IEnumerator activite()
    {
        yield return new WaitForSeconds(.3f);
        objectShow.SetActive(active);
    }
}
