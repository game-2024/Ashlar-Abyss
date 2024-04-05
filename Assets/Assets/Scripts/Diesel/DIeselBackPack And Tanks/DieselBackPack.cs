using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class DieselBackPack : MonoBehaviour
{
    // Start is called before the first frame update


    public DieselTank TestTankSpawn;
    public DieselManagerScript DieselManager;


    [Serializable] private struct DieselTankSlots
    {

        public Transform DieselTankTransform1;
        public Transform DieselTankTransform2;

        public DieselTank DieselTank1;
        public DieselTank DieselTank2;

    }

    [SerializeField] private DieselTankSlots TankSlots;



    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            DieselTank dieselTank = Instantiate(TestTankSpawn);
            TankPickedUp(dieselTank);
        }



    }


    public void TankPickedUp(DieselTank newTank)
    {

        bool swapTanks = false;

     

        //Nulled DieslTank value for the ref dieselSlotToOccupy;
        DieselTank nullDiesel = null;

        ref DieselTank dieselSlotToOccupy = ref nullDiesel;
        Transform dieselSlotTranformToOccupy = null;

        if (TankSlots.DieselTank1 == null)
        {
            dieselSlotToOccupy = ref TankSlots.DieselTank1;
            dieselSlotTranformToOccupy = TankSlots.DieselTankTransform1;

            swapTanks = true;
        }
        else if (TankSlots.DieselTank2 == null)
        {
            dieselSlotToOccupy = ref TankSlots.DieselTank2;
            dieselSlotTranformToOccupy = TankSlots.DieselTankTransform2;

            swapTanks = true;
        }
        else if (newTank.TankCurrentDiesel > TankSlots.DieselTank1.TankCurrentDiesel)
        {
            DieselManager.DieselTankRemoved(TankSlots.DieselTank1);
            Destroy(TankSlots.DieselTank1.gameObject);
            dieselSlotToOccupy = ref TankSlots.DieselTank1;
            dieselSlotTranformToOccupy = TankSlots.DieselTankTransform1;

            swapTanks = true;
        }
        else if (newTank.TankCurrentDiesel > TankSlots.DieselTank2.TankCurrentDiesel)
        {
            DieselManager.DieselTankRemoved(TankSlots.DieselTank2);
            Destroy(TankSlots.DieselTank2.gameObject);

            dieselSlotToOccupy = ref TankSlots.DieselTank2;
            dieselSlotTranformToOccupy = TankSlots.DieselTankTransform2;

            swapTanks = true;

        }
        else
        {
            Destroy(newTank.gameObject);
            return;
        }



        if (swapTanks == true)
        {
            dieselSlotToOccupy = newTank;
            dieselSlotToOccupy.transform.SetParent(dieselSlotTranformToOccupy);
            dieselSlotToOccupy.transform.localPosition = Vector3.zero;
            dieselSlotToOccupy.transform.localEulerAngles = new Vector3(0f, 0f, -90f);

            DieselManager.DieselTankGained(dieselSlotToOccupy);

        }
    }


}
