using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public static BackgroundMusic instance { get; private set; }
    public AudioSource source;
    public AudioClip[] backgroundMusics;

    private void Awake()
    {
        instance = this;
        source = GetComponent<AudioSource>();
    }

    private void Start()
    {
        foreach (AudioClip clip in backgroundMusics)
        {
            source.PlayOneShot(clip);
        }
    }
}
