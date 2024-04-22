using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Timeline;


[CreateAssetMenu(fileName = "DieselManager", menuName = "ScriptableObjects/DieselManagerScriptableObject")]


public class DieselManagerScript : ScriptableObject
{
    [SerializeField] private int maxDiesel;
    public int MaxDiesel
    {
        get { return maxDiesel; }
        private set { maxDiesel = value; }
    }

    [SerializeField]private int currentDiesel;
    public int CurrentDiesel
    {
        get { return currentDiesel; }

        set
        {
            currentDiesel = value;
            if(currentDiesel > maxDiesel)
            {
                currentDiesel = maxDiesel;
            }

            if(currentDiesel < 0)
            {
                currentDiesel = 0;
            }
        }
    }

    [HideInInspector]
    public UnityEvent<int,int> onDieselUpdated;

    private void OnEnable()
    {
        maxDiesel = 0;
        currentDiesel = maxDiesel;
    }

    public void DecreaseDieselByAmount(int amountToDecrease)
    {
        CurrentDiesel -= amountToDecrease;
        onDieselUpdated?.Invoke(currentDiesel, maxDiesel);
    }


    public void IncreaseDieselByAmount(int amountToIncrease)
    {
        CurrentDiesel += amountToIncrease;
        onDieselUpdated?.Invoke(currentDiesel, maxDiesel);
    }


    public void DieselTankGained(DieselTank newTank)
    {
        if (newTank == null)
        {
            return;
        }

        MaxDiesel += newTank.TankMaxDiesel;
        CurrentDiesel += newTank.TankCurrentDiesel;

        onDieselUpdated?.Invoke(currentDiesel, maxDiesel);

    }

    public void DieselTankRemoved(DieselTank oldTank)
    {

        if(oldTank == null)
        {
            return;
        }


        MaxDiesel -= oldTank.TankMaxDiesel;
        CurrentDiesel -= oldTank.TankCurrentDiesel;

        onDieselUpdated?.Invoke(currentDiesel, MaxDiesel);
    }

    public void InitialDieselValuesInvoke()
    {
        onDieselUpdated?.Invoke(CurrentDiesel, MaxDiesel);
    }


}


