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
        if( other.TryGetComponent<PlayerController>(out player) )
        {
            playerBackPackPosition = player.transform.Find("BackPackLocation");
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
