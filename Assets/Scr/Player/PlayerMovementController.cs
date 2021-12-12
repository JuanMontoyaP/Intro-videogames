using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    private Rigidbody _rb;
    private Vector3 _targetVelocity;

    public void Move(Vector3 velocity)
    {
        _targetVelocity = velocity;
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        _rb.velocity = _targetVelocity;
    }
}
