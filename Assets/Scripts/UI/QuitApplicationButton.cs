using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitApplicationButton : MonoBehaviour
{
    public void QuitApplication()
    {
        Debug.Log("Application Quitting");
        Application.Quit();
    }
}
