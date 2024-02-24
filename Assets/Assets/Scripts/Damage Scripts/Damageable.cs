using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour, IDamageable
{
     public UnityEvent<int> onDamaged;

    public void TakeDamage(int damage_taken)
    {
        onDamaged?.Invoke(damage_taken);
    }

}
