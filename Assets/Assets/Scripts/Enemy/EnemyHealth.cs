using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    public float MaxHealth
    {
        private set { maxHealth = value; }
        get { return currentHealth; }
    }

    private float currentHealth;
    public float CurrentHealth
    {
        private set
        {

            currentHealth = value;

            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                Destroy(this.transform.parent.gameObject);
            }
        }

        get { return currentHealth; }
    }


    private void Start()
    {
        currentHealth = maxHealth;
    }



    public void TakeDamage(float damageToTake)
    {
        CurrentHealth -= damageToTake;
    }


}
