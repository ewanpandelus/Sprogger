using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    Image image;

    [SerializeField] float minAlpha = 0, maxAlpha = 1f, fadeTime = 0.5f;

    GameObject quitButton;

    private void Awake()
    {
        image = gameObject.GetComponent<Image>();
        quitButton = transform.GetChild(0).gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        image.color = Color.clear;
    }

    public bool UIVisible = false;

    public void ShowUI(float _time)
    {
        if (!UIVisible)
        {
            LeanTween.value(gameObject, minAlpha, maxAlpha, _time).setOnUpdate((float _alpha) =>
            {
                image.color = new Color(1, 1, 1, _alpha);
            }).setEase(LeanTweenType.easeInCubic).setOnComplete(FadeInQuitButton).setDelay(0.5f) ;
            UIVisible = true;
        }
    }



    public void HideUI()
    {
        if (UIVisible)
        {
            LeanTween.value(gameObject, maxAlpha, minAlpha, fadeTime).setOnUpdate((float _alpha) =>
            {
                image.color = new Color(1, 1, 1, _alpha);
            }).setEase(LeanTweenType.easeInCubic).setDelay(0.5f).setOnComplete(DestroyThis);
            UIVisible = false;
        }
    }

    public void FadeInQuitButton()
    {
        quitButton.SetActive(true);
        LeanTween.value(quitButton, minAlpha, maxAlpha, fadeTime).setOnUpdate((float _alpha) =>
        {
            quitButton.GetComponent<Image>().color = new Color(1, 1, 1, _alpha);
        }).setEase(LeanTweenType.easeInCubic);
        UIVisible = true;
    }

    void DestroyThis()
    {
        Destroy(gameObject);
    }
}
