using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromptController : MonoBehaviour
{
    Prompt wasd;

    private void Awake()
    {
        wasd = GameObject.Find("WASDPrompt").GetComponent<Prompt>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A)|| Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            if (wasd != null)
            {
                wasd.HideUI();
            }
        }
    }
}
