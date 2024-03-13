using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GenericEnemy : MonoBehaviour
{

    [SerializeField] private float health;

    public void TakeMyDamage(float damage_taken)
    {
        health -= damage_taken;
        Debug.Log("In Generic Enemy Script of Object" + this.gameObject.name + " " + health);

        if(health <= 0)
        {
            Destroy(gameObject);
        }

    }


}
