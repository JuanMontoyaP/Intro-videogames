using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerMovementController _movementController;

    [SerializeField] private float speed = 6.5f;
    [SerializeField] private float _rotationSpeed = 30f;

    private Vector2 _movementInput;
    private Quaternion _targetRotation;
    private Camera _cam;

    void Start()
    {
        _movementController = GetComponent<PlayerMovementController>();
        _cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();

        //Movement
        Vector3 targetMovementDirection = new Vector3(_movementInput.x, 0, _movementInput.y);
        _movementController.Move(targetMovementDirection.normalized * speed);

        // _targetRotation = Quaternion.LookRotation(targetMovementDirection);
        _movementController.RotateTo(_targetRotation, _rotationSpeed);
    }

    void ProcessInput()
    {
        _movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        CalculateTargetRotation();
    }

    void CalculateTargetRotation()
    {
        Vector2 mouseScreenPosition = Input.mousePosition;
        RaycastHit hit;
        Ray ray = _cam.ScreenPointToRay(mouseScreenPosition);

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 dir = (hit.point - transform.position).normalized;
            _targetRotation = Quaternion.LookRotation(dir);
        }
    }
}
