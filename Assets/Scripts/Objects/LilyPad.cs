using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LilyPad : MonoBehaviour
{
    private Controller playerController;
    private ConstantRotation parentRotater;
    private CheckpointReset checkPointReset;
    private Checkpoint checkPoint;
    private Vector3 lilyPadScale;
    [SerializeField]
    private Material endMaterial = null;
    private Renderer rend;

    private void Start()
    {
        parentRotater = gameObject.GetComponentInParent<ConstantRotation>();
        checkPointReset = GameObject.Find("CheckpointReset").GetComponent<CheckpointReset>();
        checkPoint = gameObject.transform.parent.transform.parent.transform.parent.GetComponent<Checkpoint>();
        lilyPadScale = transform.localScale;
        rend = GetComponent<MeshRenderer>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            playerController = other.gameObject.GetComponent<Controller>();
            bool isPlayerUnderneath = CalcIfUnderneath();
            if (playerController&&!isPlayerUnderneath)
            {
                playerController.Stick(this, parentRotater.GetRotationRate());
                if (checkPoint)
                {
                    checkPoint.UpdateCheckpoint();
                }
            }
            else
            {
                checkPointReset.AddGameObjectsToReset(gameObject.transform.parent.transform.parent.gameObject);
            }
   
        } 
    }
    private bool CalcIfUnderneath()
    {
        if ((playerController.transform.position.y+playerController.GetComponent<CapsuleCollider>().radius/2) - this.transform.position.y < 0)
        {
            ScaleDown();
            return true;
        }
        return false;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            playerController.UnSitck(this);
            if (gameObject.transform.parent.transform.parent.transform.parent.tag == "EndPad")
            {
                EndLily();
                return;
            }
        
            checkPointReset.AddGameObjectsToReset(gameObject.transform.parent.transform.parent.gameObject);
            ScaleDown();
        }
    }
    private void EndLily()
    {
        Ending.Instance.UpdateEndPad(gameObject);
        if (endMaterial)
        {
            rend.material.Lerp(GetComponent<MeshRenderer>().material, endMaterial,2f);
        }
    }
    private void KillLilyPad()
    {

        gameObject.transform.parent.transform.parent.gameObject.SetActive(false);
        transform.localScale = lilyPadScale;
    }

    private void ScaleDown()
    {
        LeanTween.scale(gameObject, Vector3.zero, 0.5f).setEaseInQuad().setOnComplete(KillLilyPad);
    }
}

