using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Timeline;

public class DieselManagerScript : MonoBehaviour, IDieselAmountsMinMaxReadable, IMainDieselChangable
{
    [SerializeField] private int maxDiesel = 1000;
    public int MaxDiesel
    {
        get { return maxDiesel; }
    }

    [SerializeField] private int currentDiesel;
    public int CurrentDiesel
    {
        get 
        {
            return currentDiesel;
        }
        
        set
        {
            currentDiesel = value;
            if(currentDiesel > maxDiesel)
            {
                currentDiesel = maxDiesel;
            }
        }
    }


    [Serializable]
    public class DieselUpdated : UnityEvent<int,int> { }
    public  DieselUpdated onDieselUpdated;

    private void Start()
    {
        currentDiesel = maxDiesel;
    }

    private  void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            currentDiesel = maxDiesel;
        }
    }


    public void DecreaseDieselByAmount(int amountToDecrease)
    {
        CurrentDiesel -= amountToDecrease;

        if (onDieselUpdated != null)
        {
            onDieselUpdated.Invoke(currentDiesel, maxDiesel);
        }
    }

    public void IncreaseDieselByAmount(int amountToIncrease)
    {
        CurrentDiesel += amountToIncrease;
    }



}


