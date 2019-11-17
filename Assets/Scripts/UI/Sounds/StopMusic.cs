using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopMusic : MonoBehaviour
{
    private AudioSource musicSource;

    private void Start()
    {
        musicSource = GetComponent<AudioSource>();
    }

    public void StopAmbianceMusic()
    {
        musicSource.Stop();
    }
}
