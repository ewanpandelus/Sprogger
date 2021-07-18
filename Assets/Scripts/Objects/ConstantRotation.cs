using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ConstantRotation : MonoBehaviour
{
    [SerializeField]
    private float rotateRate;
    private float rotationR;

    private void Start()
    {
        rotationR = rotateRate;
    }

    private void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, rotateRate, 0) * Time.deltaTime);
    }
    public float GetRotationRate()
    {
        return this.rotationR;
    }
}
