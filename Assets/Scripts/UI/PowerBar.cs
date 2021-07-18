using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerBar : MonoBehaviour
{
    [SerializeField]
    private float maxAlphaPercentage = 0f;
    [SerializeField]
    private Image screenHaze = null;
    public void ResetPowerBar()
    {
        InvokeRepeating("DecreasePowerBarFill", 0.25f, 0.01f);
    }
    private void DecreasePowerBarFill()
    {
        float powerBarPercentage = screenHaze.color.a;
        powerBarPercentage -= 0.01f;
        if (powerBarPercentage > 0)
        {
            screenHaze.color = new Color(screenHaze.color.r, screenHaze.color.g, screenHaze.color.b, powerBarPercentage);
        }
        else CancelInvoke();
    }
    
    public void UpdateScreenHazeAlpha(float alphaPercentage)
    {
        screenHaze.color = new Color(screenHaze.color.r, screenHaze.color.g, screenHaze.color.b, maxAlphaPercentage*alphaPercentage);        
    }
}
