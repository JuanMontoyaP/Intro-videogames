using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerMovementController _movementController;
    private GunController _gunController;

    [SerializeField] private float speed = 6.5f;
    [SerializeField] private float _rotationSpeed = 30f;

    private Vector2 _movementInput;
    private Quaternion _targetRotation;
    private Camera _cam;
    private Plane _worldPlane;
    private bool _isShooting;

    void Start()
    {
        _movementController = GetComponent<PlayerMovementController>();
        _gunController = GetComponent<GunController>();
        _cam = Camera.main;
        _worldPlane = new Plane(Vector3.up, Vector3.zero);
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

        if (_isShooting)
        {
            _gunController.OnTriggerHold();
        }
        else
        {
            _gunController.OnTriggerRelease();
        }
    }

    void ProcessInput()
    {
        _movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        CalculateTargetRotation();

        _isShooting = Input.GetButton("Fire1");
    }

    void CalculateTargetRotation()
    {
        Vector2 mouseScreenPosition = Input.mousePosition;
        // RaycastHit hit;
        Ray ray = _cam.ScreenPointToRay(mouseScreenPosition);

        // if (Physics.Raycast(ray, out hit))
        // {
        //     Vector3 dir = (hit.point - transform.position).normalized;
        //     _targetRotation = Quaternion.LookRotation(dir);
        // }

        if (_worldPlane.Raycast(ray, out float distanceToPlane))
        {
           Vector3 pointHit = ray.GetPoint(distanceToPlane);
           Vector3 dir = (pointHit - transform.position).normalized;
            _targetRotation = Quaternion.LookRotation(dir);
        }
    }
}
