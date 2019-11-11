using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField]
    AudioClip introMusic;

    [SerializeField]
    AudioClip musicLoop;

    [SerializeField]
    AudioSource musicSource;


    [SerializeField]
    AudioSource sfxSource;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(switchToChaseMusic());
    }

    IEnumerator switchToChaseMusic()
    {
        yield return new WaitForSeconds(introMusic.length);
        musicSource.clip = musicLoop;
        musicSource.loop = true;
        musicSource.Play();
    }
}
