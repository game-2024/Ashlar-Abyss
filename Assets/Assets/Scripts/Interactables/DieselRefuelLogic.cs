using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieselRefuelLogic : MonoBehaviour
{

    [SerializeField][Range(1,50)] int DieselRefuelAmount = 1;


    DieselBackPack playersDieselBackPack = null;

    public void RefuelPlayer()
    {
        if(playersDieselBackPack != null)
        {
            playersDieselBackPack.RefuelDiesel(DieselRefuelAmount);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();

        if (other.GetComponent<PlayerController>() != null)
        {
            playersDieselBackPack = other.gameObject.GetComponentInChildren<DieselBackPack>();
        }
    }


    private void OnTriggerExit(Collider other)
    {
        
        if (other.GetComponent<PlayerController>() != null)
        {
            playersDieselBackPack = null;
        }


    }




}
