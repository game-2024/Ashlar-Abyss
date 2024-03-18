using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieselRefuelLogic : MonoBehaviour
{

    [SerializeField][Range(1,50)] int DieselRefuelAmount = 1;


    DieselManagerScript PlayerDieselManager;

    public void RefuelPlayer()
    {
        PlayerDieselManager.IncreaseDieselByAmount(DieselRefuelAmount);
    }


    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();

        if (player != null)
        {
            PlayerDieselManager = player.DieselManagerProperty;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            PlayerDieselManager = null;
        }


    }




}
