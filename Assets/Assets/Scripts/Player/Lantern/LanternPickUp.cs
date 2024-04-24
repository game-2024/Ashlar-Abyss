using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using UnityEngine;

public class LanternPickUp : MonoBehaviour
{
    [SerializeField] LanternScript lantern;
    Transform lanternLocation;
    PlayerController player;




    public void PerformInteraction()
    {

        if(lanternLocation != null && player != null)
        {
            lantern.transform.SetParent(lanternLocation.parent);
            lantern.transform.localPosition = lanternLocation.localPosition;
            lantern.transform.localRotation = lanternLocation.localRotation;
            lantern.transform.localScale = lanternLocation.localScale;

            player.EquipLantern(ref lantern);

            Destroy(lanternLocation.gameObject);
            Destroy(this.gameObject);

        }


    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.TryGetComponent<PlayerController>(out player))
        {
            if (other.GetComponentInChildren<LanternLocation>() != null)
            {
                LanternLocation lanternTransform = other.GetComponentInChildren<LanternLocation>();
                lanternLocation = lanternTransform.transform;
            }
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
