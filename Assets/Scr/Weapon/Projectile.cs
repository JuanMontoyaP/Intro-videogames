using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed = 10;
    [SerializeField] private float _lifeTime = 3;

    private float _timeToDisable = 3;

    void Start()
    {
        _timeToDisable = _lifeTime;
    }

    void Update()
    {
        if (_timeToDisable <= 0)
        {
            Destroy(gameObject);
            return;
        }

        _timeToDisable -= Time.deltaTime;

        float movementDistance = _speed * Time.deltaTime;
        Vector3 translation = Vector3.forward * movementDistance;
        transform.Translate(translation);
        CheckCollision(translation);
    }

    private void CheckCollision(Vector3 translation)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, translation.magnitude))
        {
            Destroy(gameObject);
        }
    }
}
