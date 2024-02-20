using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponScript : MonoBehaviour, IDamager
{

    [SerializeField] private int damageToDeal;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void DealDamage(Collider collided_with)
    {
        IDamageable collision = collided_with.gameObject.GetComponent<IDamageable>();

        collision?.TakeDamage(damageToDeal);
    }
}
