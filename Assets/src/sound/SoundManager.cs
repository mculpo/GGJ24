using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public AudioClip click_card;
    public AudioClip shuffle_card;
    public AudioClip check_win;
    public AudioClip public_laugh;
    public AudioClip add_points;

    private AudioSource audioSource;


    void Awake()
    {
        // Obtém ou cria um componente AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }
    public void PlaySoundAddPoints()
    {
        PlayOneShot(add_points);
    }
    public void PlaySoundShuffleCard()
    {
        PlayOneShot(shuffle_card);
    }
    public void PlaySoundClickCard()
    {
        PlayOneShot(click_card);
    }

    public void PlaySoundCheckWin()
    {
        PlayOneShot(check_win);
    }

    public void PlaySoundPublicLaugh()
    {
        PlayOneShot(public_laugh);
    }


    // Adicione mais métodos conforme necessário...

    private void PlayOneShot(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("Som ausente. Certifique-se de atribuir um AudioClip no Editor Unity.");
        }
    }
}
