using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkSound : MonoBehaviour
{
    private Controller playerController;
    private AudioSource audioSource;
    bool isPlaying = false;

    private void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<Controller>();
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    private void Update()
    {
        Walk();
    }


    private void Walk()
    {
        bool onGround = playerController.GetOnGround();
        Vector3 velocity = playerController.GetRigidbody().velocity;
        if (onGround && velocity.magnitude>0)
        {
            audioSource.pitch = ModeratePitch(velocity.magnitude);
            if (!isPlaying)
            {
               
                audioSource.Play();
                isPlaying = true;
            }
        }
        else
        {
            if (isPlaying)
            {
                audioSource.Stop();
                isPlaying = false;
            }
        }
    }
    private float ModeratePitch(float velocityMag)
    {
        if (velocityMag <= 1.5)
        {
            return velocityMag;
        }
        else
        {
            return 1.5f;
        }
    }


}
