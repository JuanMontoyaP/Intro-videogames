using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform _projectileSpawnPoint;
    [Header("Testing...")]
    public Projectile projectilePrefab;

    public void Shoot()
    {
        // Debug.LogError("Piu Piu -> ->");
        if (projectilePrefab == null)
        {
            projectilePrefab = Resources.Load<Projectile>("Prefabs/Projectile");
        }

        Quaternion projectileRotation = Quaternion.LookRotation(_projectileSpawnPoint.forward);        
        Instantiate(projectilePrefab, _projectileSpawnPoint.position, projectileRotation);

    }
}
