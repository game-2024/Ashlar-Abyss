using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static DieselManagerScript;

public class healthSystemScript : MonoBehaviour
{
    [SerializeField] public int maxHealth = 100;
    [SerializeField] public int currHealth;
    [SerializeField] int damage = 1;//remove after testing
    [SerializeField] int heal = 1;//remove after testing


    [Serializable]
    public class HealthAdjusted : UnityEvent<int,int>{}
    public HealthAdjusted onHealthAdjust;

    public int HealthStart()
    {
        currHealth = maxHealth;
        return currHealth;
    }

    public int HealPlayer(int healing)//Healing system for player
    {
        if (currHealth < maxHealth)
        {
            currHealth += healing;
            //Revisit: consume health item
            return currHealth;
        }
        else
        {
            //Revisit: print text to UI
            return currHealth;
        }
    }

    public int DamagePlayer(int damage)//Damage system for player
    {
        if (currHealth > 0)
        {
            currHealth -= damage;
            //Revisit: add invincibility frames
            return currHealth;
        }
        else
        {
            //Revisit: run player death function
            return 0;
        }
    }

    private void tempHealthTest()//delete after testing
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            HealPlayer(heal);
            if(onHealthAdjust != null)
            {
                onHealthAdjust.Invoke(currHealth, maxHealth);
            }
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            DamagePlayer(damage);
            if (onHealthAdjust != null)
            {
                onHealthAdjust.Invoke(currHealth, maxHealth);
            }
        }
    }

    void Start()
    {
        HealthStart();
    }

    
    void Update()
    {
        tempHealthTest();
    }
}
