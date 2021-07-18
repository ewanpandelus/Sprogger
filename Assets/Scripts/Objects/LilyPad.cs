using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LilyPad : MonoBehaviour
{
    private Controller playerController;
    private ConstantRotation parentRotater;
    private CheckpointReset checkPointReset;
    private Checkpoint checkPoint;
    

    Vector3 lillipadScale;

    private void Start()
    {
        parentRotater = gameObject.GetComponentInParent<ConstantRotation>();
        checkPointReset = GameObject.Find("CheckpointReset").GetComponent<CheckpointReset>();
        checkPoint = gameObject.transform.parent.transform.parent.transform.parent.GetComponent<Checkpoint>();
        lillipadScale = transform.localScale;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            playerController = other.gameObject.GetComponent<Controller>();
            if (playerController)
            {
                playerController.gameObject.AddComponent<ConstantRotation>();
                playerController.Stick(this, parentRotater.GetRotationRate());
                if (checkPoint)
                {
                    checkPoint.UpdateCheckpoint();
                }
            }
        } 
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.name == "Player")
        {
            playerController.UnSitck(this);
            checkPointReset.AddGameObjectsToReset(gameObject.transform.parent.transform.parent.gameObject);
            ScaleDown();
            if(gameObject.transform.parent.transform.parent.transform.parent.tag == "EndPad")
            {
                Ending.Instance.UpdateEndPad(gameObject);
            }
      

        }
    }

    private void KillLilyPad()
    {

        gameObject.transform.parent.transform.parent.gameObject.SetActive(false);
        transform.localScale = lillipadScale;
    }

    private void ScaleDown()
    {
        LeanTween.scale(gameObject, Vector3.zero, 0.5f).setEaseInQuad().setOnComplete(KillLilyPad);
    }
}

