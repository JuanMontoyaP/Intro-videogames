using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private float _fireRate = 0.1f;
    [SerializeField] private Transform _projectileSpawnPoint;
    [Header("Testing...")] private Projectile projectilePrefab;

    private float _nextShootAt = 0;

    public void OnTriggerHold()
    {
        if (Time.time > _nextShootAt)
        {
            _nextShootAt = Time.time + _fireRate;
            Shoot();
        }
        
    }

    public void OnTriggerRelease()
    {

    }

    public void Shoot()
    {
        // Debug.LogError("Piu Piu -> ->");
        if (projectilePrefab == null)
        {
            projectilePrefab = Resources.Load<Projectile>("Prefabs/Projectile");
        }

        Projectile projectileObject = Instantiate(projectilePrefab);
        projectileObject.transform.position = _projectileSpawnPoint.position;
        projectileObject.transform.rotation = Quaternion.LookRotation(_projectileSpawnPoint.forward);  
    }
}
