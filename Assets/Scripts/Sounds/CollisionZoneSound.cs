using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionZoneSound : MonoBehaviour
{
    [SerializeField]
    private bool shouldFade = false;
    private bool playedOnce = false;
    private AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player"&&!playedOnce)
        {
            PlayAudioSource();
            playedOnce = true;
        }
    }
    private void PlayAudioSource()
    {
       audioSource.Play();
       if (shouldFade)
       {
           StartCoroutine(StartFade(audioSource,audioSource.clip.length, 0f));
       }
        
    }
    public IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }
}
