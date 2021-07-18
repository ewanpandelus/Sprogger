using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookUiTrigger : MonoBehaviour
{
    Prompt prompt;

    private void Awake()
    {
        prompt = GameObject.Find("LookPrompt").GetComponent<Prompt>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && prompt != null)
        {
            prompt.ShowUI();
        }
    }

    private void Update()
    {
        if (prompt != null)
        {
            if (prompt.UIVisible && Input.GetKeyDown(KeyCode.Space))
            {
                prompt.HideUI();
            }
        }
    }
}
