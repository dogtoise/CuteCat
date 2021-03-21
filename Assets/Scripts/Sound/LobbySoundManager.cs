using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbySoundManager : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip backgroundMusic;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = backgroundMusic;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
