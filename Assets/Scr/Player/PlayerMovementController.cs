using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    private Rigidbody _rb;
    private Transform _body;
    private Vector3 _targetVelocity;
    private Quaternion _targetRotation;
    private float _targetRotationSpeed;

    public void Move(Vector3 velocity)
    {
        _targetVelocity = velocity;
    }

    public void RotateTo(Quaternion rotation, float rotationSpeed)
    {
        _targetRotation = rotation;
        _targetRotationSpeed = rotationSpeed;
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _body = transform.Find("PlayerBody");
    }

    void Update()
    {
        _body.rotation = Quaternion.RotateTowards(_body.rotation, _targetRotation, _targetRotationSpeed) ;
    }

    void FixedUpdate()
    {
        _rb.velocity = _targetVelocity;
    }
}
