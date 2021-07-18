using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    private Controller playerController;
    [SerializeField]
    private float bounceMagnitude;
    private CheckpointReset checkPointReset;
    private void Start()
    {
        checkPointReset = GameObject.Find("CheckpointReset").GetComponent<CheckpointReset>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            playerController = other.gameObject.GetComponent<Controller>();
            if (playerController) 
            { 
                playerController.LeapForce(playerController.GetLastLeapForce()*bounceMagnitude);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            checkPointReset.AddGameObjectsToReset(gameObject);
            gameObject.SetActive(false);
  
        }
    }
}
