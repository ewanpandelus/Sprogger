using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private CheckpointReset checkPointReset;
    private CheckpointSound checkPointSound;
    [SerializeField]
    private bool shouldUpdateCheckpoint = true;
    private bool hasCollided = false;
    private void Start()
    {
        checkPointReset = GameObject.Find("CheckpointReset").GetComponent<CheckpointReset>();
        checkPointSound = GetComponent<CheckpointSound>();
    }
   
    public void UpdateCheckpoint()
    {
        if (shouldUpdateCheckpoint&&!hasCollided)
        {
            hasCollided = true;
            if (checkPointReset && checkPointSound)
            {
                checkPointSound.PlaySound();
                checkPointReset.ClearGameObjectsToReset();
                checkPointReset.SetCurrentCheckPoint(checkPointReset.GetCurrentCheckPoint() + 1);
            }
          
        }
    }
}