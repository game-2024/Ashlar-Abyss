using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieselBackPackPickup : MonoBehaviour
{

    [SerializeField] DieselBackPack dieselBackPack;
    Transform playerBackPackPosition;
    PlayerController player;


    public void OnInteractionPerformed()
    {

        if(playerBackPackPosition != null)
        {
            dieselBackPack.transform.SetParent(playerBackPackPosition.parent);
            dieselBackPack.transform.localPosition = playerBackPackPosition.localPosition;
            dieselBackPack.transform.localRotation = playerBackPackPosition.localRotation;


            Destroy(playerBackPackPosition.gameObject);
            Destroy(this);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if( other.GetComponentInChildren<BackPackLocation>() != null )
        {
            BackPackLocation location = other.GetComponentInChildren<BackPackLocation>();
            playerBackPackPosition = location.transform;
            //Debug.Log("Position Found");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.GetComponent<PlayerController>() != null)
        {
            player = null;
        }
    }
}
