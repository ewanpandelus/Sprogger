using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingRubbish : MonoBehaviour
{
    Vector3 torque;
    [SerializeField] float torqueAmount = 10f;
    Rigidbody rb;

    private void Awake()
    {
        rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
        torque = new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)) * torqueAmount;
    }
    void Start()
    {
        rb.AddTorque(torque,ForceMode.Impulse);
    }

}
