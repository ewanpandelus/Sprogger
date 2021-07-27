using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    private Controller playerController;
    private Rigidbody orbittingRigidBody;
    [SerializeField]
    private float atmosphericRadius = 1000000f;
    private float magnitude = 0.5f;
    private bool shouldPull = false;

    private void Start()
    {
        playerController = FindObjectOfType<Controller>();
        orbittingRigidBody = playerController.GetRigidbody();



    }

    private void FixedUpdate()
    {
        if (shouldPull)
        {
            if (IsObjectOrbitting())
            {
                ApplyGravitationalForce();
            }
        }
      
    }
    private bool IsObjectOrbitting()
    {
        float dist = Vector3.Distance(this.transform.position, playerController.transform.position);
        if (dist < 140f)
        {
            playerController.enabled = false;
            FadePanel.Instance.FadeToBlack(1f);
            GameObject.Find("EndScreen").GetComponent<EndScreen>().ShowUI(1f);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        if (dist < atmosphericRadius)
        {
            return true;
        }
        return false;
    }
    private void ApplyGravitationalForce()
    {
        orbittingRigidBody.AddForce(magnitude * (transform.position - playerController.transform.position));
    }
    public void SetShouldPull(bool _shouldPull)
    {
        shouldPull = _shouldPull;
    }
}
