using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable
{
    [SerializeField] [Range(1, 5)]
    protected int _totalHealth = 2;
    protected int _currentHealth;
    protected bool _isDead;

    public int TotalHealth => _totalHealth;
    public int CurrentHealth => _currentHealth;
    public bool IsDead => _isDead;
    public Action<int> OnTakeHit { get; set; }
    public Action OnDeath { get; set; }
    

    private void Start()
    {
        _currentHealth = _totalHealth;
        _isDead = false;
    }

    public void TakeHit(int damage, Vector3 hitPoint, Vector3 hitDirection)
    {
        if (_isDead)
        {
            return;
        }
        // TODO: Add VFX
        ApplyDamage(damage);
        OnTakeHit?.Invoke(damage);
    }

    public void ApplyDamage(int damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0 && !_isDead)
        {
            Died();
        }
    }

    public void Died()
    {
        _isDead = true;
        Debug.LogError("I'm dead!");

        OnDeath?.Invoke();
    }
}
