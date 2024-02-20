using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GenericEnemy : MonoBehaviour, IDamageable
{

    [SerializeField] private int health;

    public void TakeDamage(int damage_taken)
    {
        health -= damage_taken;
        Debug.Log("In Generic Enemy Script of Object" + this.gameObject.name + " " + health);

        if(health <= 0)
        {
            Destroy(gameObject);
        }

    }


}
