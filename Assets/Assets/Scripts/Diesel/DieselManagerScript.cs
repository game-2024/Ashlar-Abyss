using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Timeline;



[CreateAssetMenu(fileName = "DieselManager", menuName = "ScriptableObjects/DieselManagerScriptableObject")]

public class DieselManagerScript : ScriptableObject
{
    [SerializeField] private int maxDiesel = 1000;
    public int MaxDiesel
    {
        get { return maxDiesel; }
    }

    [SerializeField] private int currentDiesel;
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


    public UnityEvent<int,int> onDieselUpdated;


    private void OnEnable()
    {
        currentDiesel = maxDiesel;
        onDieselUpdated?.Invoke(CurrentDiesel, MaxDiesel);
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



}


