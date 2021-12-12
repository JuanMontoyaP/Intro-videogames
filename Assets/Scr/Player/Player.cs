using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerMovementController _movementController;

    [SerializeField] private float speed = 6.5f;
    [SerializeField] private float _rotationSpeed = 30f;

    private Vector2 _movementInput;

    void Start()
    {
        _movementController = GetComponent<PlayerMovementController>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();

        //Movement
        Vector3 targetMovementDirection = new Vector3(_movementInput.x, 0, _movementInput.y);
        _movementController.Move(targetMovementDirection.normalized * speed);

        Quaternion targetRotation = Quaternion.LookRotation(targetMovementDirection);
        _movementController.RotateTo(targetRotation, _rotationSpeed);
    }

    void ProcessInput()
    {
        _movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
}
