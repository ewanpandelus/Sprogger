using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    private Controller playerController;
    private Rigidbody orbittingRigidBody;
    [SerializeField]
    private float atmosphericRadius;
    [SerializeField]
    private float magnitude;

    private void Start()
    {
        playerController = FindObjectOfType<Controller>();
        orbittingRigidBody = playerController.GetRigidbody();
    }

    private void FixedUpdate()
    {
        IsObjectOrbitting();
    }
    private void IsObjectOrbitting()
    {
        float dist = Vector3.Distance(this.transform.position, playerController.transform.position);
        if (dist < atmosphericRadius)
        {
            ApplyGravitationalForce(dist);
        }
        
    }
    private void ApplyGravitationalForce(float dist)
    {
        float totalForce = magnitude *((1 / dist) * atmosphericRadius);
        orbittingRigidBody.AddForce(totalForce * (transform.position - playerController.transform.position));
    }
}
