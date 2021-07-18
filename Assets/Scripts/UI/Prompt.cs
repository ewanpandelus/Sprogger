using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Prompt : MonoBehaviour
{
    Image image;

    [SerializeField] float minAlpha = 0, maxAlpha = 1f, fadeTime = 0.5f;

    private void Awake()
    {
        image = gameObject.GetComponent<Image>();
    }

    // Start is called before the first frame update
    void Start()
    {
        image.color = Color.clear;
    }

    public bool UIVisible = false;

    public void ShowUI()
    {
        if (!UIVisible)
        {
            LeanTween.value(gameObject, minAlpha, maxAlpha, fadeTime).setOnUpdate((float _alpha) =>
            {
                image.color = new Color(1, 1, 1, _alpha);
            }).setEase(LeanTweenType.easeInCubic);
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

    void DestroyThis()
    {
        Destroy(gameObject);
    }
}
