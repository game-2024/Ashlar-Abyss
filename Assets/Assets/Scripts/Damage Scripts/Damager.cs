using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour, IDamager
{

    [SerializeField] int damageToDeal;

    public void DealDamage(Collider collided_with)
    {
        Damageable collision = collided_with.gameObject.GetComponent<Damageable>();

        if(collision != null)
        {
            collision.TakeDamage(damageToDeal);
        }

    }
}
