using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource m_AudioSource;

    // Start is called before the first frame update
    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
        Play();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // plays the main level theme
    public void Play()
    {
        m_AudioSource.Play();
    }

    public void PlayOneShot(AudioClip _ClipToPlay)
    {
        m_AudioSource.PlayOneShot(_ClipToPlay);
    }

    public void Stop()
    {
        m_AudioSource.Stop();
    }
}
