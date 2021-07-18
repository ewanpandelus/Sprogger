using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadePanel : MonoBehaviour
{
    private static FadePanel _instance;

    public static FadePanel Instance { get { return _instance; } }

    float minAlpha = 0, maxAlpha = 1, fadeTime = 0.2f;
    Image panel;


    private void Awake()
    {
        if(_instance == null && _instance == this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        panel = gameObject.GetComponent<Image>();


    }

    private void Start()
    {
        Debug.Log("Start");
        FadeIn(1f);
    }

    bool isBlack = true;

    public void FadeToBlack(float _time)
    {
        if (!isBlack)
        {
            LeanTween.value(gameObject, minAlpha, maxAlpha, _time).setOnUpdate((float _alpha) =>
            {
                panel.color = new Color(0, 0, 0, _alpha);
            }).setEase(LeanTweenType.easeInCubic).setOnComplete(QuitApplication);
            isBlack = true;
        }
    }
    private void QuitApplication()
    {
        Application.Quit();
    }

    public void FadeIn()
    {
        Debug.Log("Fading In");
        if (isBlack)
        {
            LeanTween.value(gameObject, maxAlpha, minAlpha, fadeTime).setOnUpdate((float _alpha) =>
                {
                    panel.color = new Color(0, 0, 0, _alpha);
                }).setEase(LeanTweenType.easeInCubic);
            isBlack = false;
        }
        
    }

    public void FadeIn(float _time)
    {
        if (isBlack)
        {
            LeanTween.value(gameObject, maxAlpha, minAlpha, _time).setOnUpdate((float _alpha) =>
            {
                panel.color = new Color(0, 0, 0, _alpha);
            }).setEase(LeanTweenType.easeInCubic);
            isBlack = false;
        }
        
    }

    public void FadeOut()
    {
        if (!isBlack)
        {
            LeanTween.value(gameObject, minAlpha, maxAlpha, fadeTime).setOnUpdate((float _alpha) =>
            {
                panel.color = new Color(0, 0, 0, _alpha);
            }).setEase(LeanTweenType.easeInCubic).setOnComplete(RespawnPlayer);
            isBlack = true;
        }

    }
    public void FadeOut(float _time)
    {
        if (!isBlack)
        {
            LeanTween.value(gameObject, minAlpha, maxAlpha, _time).setOnUpdate((float _alpha) =>
            {
                panel.color = new Color(0, 0, 0, _alpha);
            }).setEase(LeanTweenType.easeInCubic).setOnComplete(RespawnPlayer);
            isBlack = true;
        }

    }

    private void RespawnPlayer()
    {
        GameObject.Find("Player").GetComponent<Controller>().Respawn();
    }
}
