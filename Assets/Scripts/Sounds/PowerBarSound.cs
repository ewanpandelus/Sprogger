using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBarSound : MonoBehaviour
{
    AudioSource audioSource;
    private bool isPlaying = false;
    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }
  
    public void PlaySound()
    {
        if (!isPlaying)
        {
            audioSource.Play();
            isPlaying = true;
        }      
    }
    public void StopSound()
    { 
        audioSource.Stop();
        isPlaying = false;
    }
}
