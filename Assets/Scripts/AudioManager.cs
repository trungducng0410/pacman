using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource startMusic;
    [SerializeField] private AudioSource mainMusic;

    // Start is called before the first frame update
    void Start()
    {
        startMusic.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!startMusic.isPlaying && !mainMusic.isPlaying)
        {
            mainMusic.Play();
        }

    }
}
