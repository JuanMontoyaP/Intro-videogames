using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [Header("Testing...")]
    public Gun _currentGun;

    public void OnTriggerHold()
    {
        _currentGun.Shoot();
    }
}
