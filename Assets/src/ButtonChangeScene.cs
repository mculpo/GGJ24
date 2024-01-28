using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonChangeScene : MonoBehaviour
{
    public string nameScene;
    private void OnMouseDown()
    {
        SoundManager.instance.PlaySoundClickCard();
        StartCoroutine(playScene());
    }

    IEnumerator playScene()
    {
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene(nameScene);
    }
}
