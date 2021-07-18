using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PressAnyKeyTween : MonoBehaviour
{
    Image _text;
    [SerializeField] Color _textColour;
    //Animatiion Properties
    [SerializeField] float _fadeInOutTime = 0.5f, _maxAlpha = 1f, _minAlpha = 0f;
    float _alpha;

    private void Awake()
    {
        _text = gameObject.GetComponent<Image>();
    }

    private void Start()
    {
        FadeIn();
    }

    private void Update()
    {
        if (Input.anyKey)
        {
            Destroy(gameObject);
        }
    }

    void FadeIn()
    {
        LeanTween.value(gameObject, _minAlpha, _maxAlpha, _fadeInOutTime).setOnUpdate((float _alpha) =>
        {
            _text.color = new Color(_textColour.r, _textColour.g, _textColour.b, _alpha);
        }).setEase(LeanTweenType.easeInCubic).setOnComplete(LoopingFadeInOutAnim);
    }

    void LoopingFadeInOutAnim()
    {
        LeanTween.value(gameObject, _maxAlpha, _minAlpha, _fadeInOutTime).setOnUpdate((float _alpha) =>
        {
            _text.color = new Color(_textColour.r, _textColour.g, _textColour.b, _alpha);
        }).setLoopPingPong().setEaseInOutSine(); ;
    }

}
