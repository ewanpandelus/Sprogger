using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleBehaviour : MonoBehaviour
{
    Material material; 
    [SerializeField] float fadeTime = 1f;

    private void Awake()
    {
        material = gameObject.GetComponent<MeshRenderer>().material;
    }
    public void FadeOut()
    {

        LeanTween.value(gameObject, 1, 0f, fadeTime).setOnUpdate((float _alpha) =>
        {
            material.SetFloat("_Alpha", _alpha);
        }).setOnComplete(DestroyThis);

    }

    void DestroyThis()
    {
        Destroy(gameObject);
    }
}
