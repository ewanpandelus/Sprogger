using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetUITrigger : MonoBehaviour
{
    Prompt prompt;

    private void Awake()
    {
        prompt = GameObject.Find("ResetPrompt").GetComponent<Prompt>();
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
            if (prompt.UIVisible)
            {
                if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
                {
                    prompt.HideUI();
                }
            }
        }
    }
}
