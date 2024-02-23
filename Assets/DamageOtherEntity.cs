using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOtherEntity : MonoBehaviour, IDamager
{

    [SerializeField] int damageToDeal;

    public void DealDamage(Collider collided_with)
    {
        IDamageable collision = collided_with.gameObject.GetComponent<IDamageable>();

        collision?.TakeDamage(damageToDeal);
    }
}
