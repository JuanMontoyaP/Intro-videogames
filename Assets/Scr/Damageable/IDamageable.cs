using System;
using UnityEngine;

public interface IDamageable
{
    int TotalHealth { get; }
    int CurrentHealth { get; }
    bool IsDead { get; }
    Action OnDeath { get; set; }

    void TakeHit(int damage, Vector3 hitPoint, Vector3 hitDirection);
    void ApplyDamage(int damage);
    void Died();
}
