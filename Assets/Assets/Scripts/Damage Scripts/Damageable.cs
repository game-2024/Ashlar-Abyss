using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour, IDamageable
{
     public UnityEvent<float> onDamaged;

    public void TakeDamage(float damage_taken)
    {
        onDamaged?.Invoke(damage_taken);
    }

}
