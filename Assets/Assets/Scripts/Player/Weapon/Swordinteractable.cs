using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swordinteractable : MonoBehaviour
{

    
    [SerializeField] PlayerWeaponScript playerWeapon;
    Transform playerWeaponLocation;
    PlayerController player;

    public void OnInteraction()
    {
        if(playerWeaponLocation != null && player != null)
        {
            playerWeapon.transform.SetParent(playerWeaponLocation.parent);
            playerWeapon.transform.localPosition = playerWeaponLocation.localPosition;
            playerWeapon.transform.localRotation = playerWeaponLocation.localRotation;
            playerWeapon.transform.localScale = playerWeaponLocation.localScale;

            player.EquipSword(ref playerWeapon);

            Destroy(playerWeaponLocation.gameObject);
            Destroy(this.gameObject);

        }
    }



    private void OnTriggerEnter(Collider other)
    {

        if (other.TryGetComponent<PlayerController>(out player))
        {
            if (other.GetComponentInChildren<SwordLocation>() != null)
            {
                SwordLocation swordLocation = other.GetComponentInChildren<SwordLocation>();
                playerWeaponLocation = swordLocation.transform;
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            playerWeaponLocation = null;
        }
    }
}