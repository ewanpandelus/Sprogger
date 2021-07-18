using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reticle : MonoBehaviour
{

    Image _text;
    [SerializeField] Color _textColour;
    //Animatiion Properties
    [SerializeField] float _fadeInOutTime = 0.5f, _maxAlpha = 1f, _minAlpha = 0f;
    float _alpha;

    private void Awake()
    {
        _text = gameObject.GetComponent<Image>();
        _text.color = Color.clear;
    }


    public void FadeInUI()
    {
        LeanTween.value(gameObject, _minAlpha, _maxAlpha, _fadeInOutTime).setOnUpdate((float _alpha) =>
        {
            _text.color = new Color(_textColour.r, _textColour.b, _textColour.g, _alpha);
        }).setEase(LeanTweenType.easeInCubic);
    }
}
