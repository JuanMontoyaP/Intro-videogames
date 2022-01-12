using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : IDamageable
{
    protected int _totalHealth;
    protected int _currentHealth;
    protected bool _isDead;


    public int TotalHealth => _totalHealth;
    public int CurrentHealth => _currentHealth;
    public bool IsDead => _isDead;

    public void TakeHit(int damage, Vector3 hitPoint, Vector3 hitDirection)
    {
        ApplyDamage(damage);
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
    }
}
