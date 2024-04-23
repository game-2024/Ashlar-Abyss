using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour, IDamager
{

    [SerializeField] private float damageToDeal;

    public float DamageToDeal
    {
        set { damageToDeal = value; }
        get { return damageToDeal; }
    }

    private void Start()
    {
        DamageToDeal = damageToDeal;   
    }



    public void DealDamage(Damageable damagedEntity)
    {
        damagedEntity.TakeDamage(DamageToDeal);
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Damageable>(out Damageable damageable))
        {
            DealDamage(damageable);
        }
    }

}
