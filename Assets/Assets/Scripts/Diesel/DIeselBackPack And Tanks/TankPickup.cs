using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankPickup : MonoBehaviour
{

    [SerializeField] DieselTank PickUpTank;

    DieselBackPack playersDieselBackPack = null;

    public void PickUpInteracted()
    {
        if(playersDieselBackPack != null)
        {
            playersDieselBackPack?.TankPickedUp(PickUpTank);
            Destroy(this.gameObject);
        }
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.GetComponent<PlayerController>() != null)
        {
            playersDieselBackPack = other.gameObject.GetComponentInChildren<DieselBackPack>();
        }

    }

    private void OnTriggerExit(Collider other)
    {
        
        if(other.GetComponent<PlayerController>() != null)
        {
            playersDieselBackPack = null;
        }


    }




}
