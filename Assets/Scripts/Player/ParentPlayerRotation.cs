using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentPlayerRotation : MonoBehaviour
{
    private bool shouldRotate = false;
    private float rotateRate;
    private void FixedUpdate()
    {
        if (shouldRotate)
        {
            transform.Rotate(new Vector3(0, rotateRate, 0) * Time.deltaTime);
        }
    }
 
    public void SetRotateRate(float _rotateRate)
    {
        this.rotateRate = _rotateRate;
    }
    public void SetShouldRotate(bool _shouldRotate)
    {
        shouldRotate = _shouldRotate;
    }
}
