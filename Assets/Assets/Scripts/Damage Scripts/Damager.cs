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



    public void DealDamage(Collider collided_with)
    {
        Damageable collision = collided_with.gameObject.GetComponent<Damageable>();


        if(collision != null)
        {
            //Debug.Log(collision.gameObject.name);
            collision.TakeDamage(DamageToDeal);
        }

    }
}
