using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    private PowerBarSound powerBarSound;
    private DeathSound deathSound;
    // Start is called before the first frame update
    void Start()
    {
        powerBarSound = GameObject.Find("Audio_PowerBar").GetComponent<PowerBarSound>();
        deathSound = GameObject.Find("Audio_Death").GetComponent<DeathSound>();
    }

    public void PlayPowerBarSound()
    {
        if (powerBarSound)
        {
            powerBarSound.PlaySound();
        }
    }
    public void StopPowerBarSound()
    {
        if (powerBarSound)
        {
            powerBarSound.StopSound();
        }
    }
    public void PlayDeathSound()
    {
        if (deathSound)
        {
            deathSound.PlaySound();
        }
    }
}
