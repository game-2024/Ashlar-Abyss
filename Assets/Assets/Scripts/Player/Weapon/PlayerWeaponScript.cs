using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponScript : MonoBehaviour
{

    [Header("References")]
        [SerializeField] DieselManagerScript DieselManager;
        [SerializeField] TimeIntervalComponentScript DieselDrainTimer;
        [SerializeField] Damager DamagerComponent;

    [Space]

    [Header("Weapon Light Refernces And Settings")]
        [SerializeField] Light WeaponLight;
        [SerializeField] float MaxLightIntensity;
        [SerializeField] float LightIntensityIncreaseScaler;

    [Space]

    [Header("Weapon Damage Stats")]
        [SerializeField] private int DamageToDeal;
        [SerializeField] private float DamageScaler;
        [Space]
        [SerializeField] private int HeatedDamageToDeal;
        [SerializeField] private float HeatedDamageScaler;

    [Space]

    [Header("Diesel Drain Stats")]
        [SerializeField] private int DieselDrainRate;



    //Non-seralized Private Variables
    private int CurrentDamageAmountToDeal;
    private float CurrentDamageAmountScaler;



    private bool WeaponHeatedToggledOn = false;


    private void Start()
    {
        CurrentDamageAmountToDeal = this.DamageToDeal;
        CurrentDamageAmountScaler = DamageScaler;

        DamagerComponent.DamageToDeal = CurrentDamageAmountToDeal * CurrentDamageAmountScaler;
    }


    private void Update()
    {
        if (WeaponHeatedToggledOn == true && DieselManager.CurrentDiesel > 0)
        {
            IncreaseWeaponLight();
        }
        else
        {
            DecreaseWeaponLight();
        }
    }

    public void ToggleWeaponHeated()
    {

        WeaponHeatedToggledOn = !WeaponHeatedToggledOn;

        if (WeaponHeatedToggledOn == true)
        {
            Debug.Log("Toggled");
            DieselDrainTimer.ResumeTimer();
            SetHeatedWeaponSettings();
           // WeaponLight
        }
        else
        {
            DieselDrainTimer.PauseTimer();
            SetNonHeatedWeaponSettings();
        }
    }


    #region Set Weapon Damage Based Off Current Active State

    private void SetHeatedWeaponSettings()
    {
        CurrentDamageAmountToDeal = HeatedDamageToDeal;
        CurrentDamageAmountScaler = HeatedDamageScaler;

        DamagerComponent.DamageToDeal = CurrentDamageAmountToDeal * CurrentDamageAmountScaler;

    }

    private void SetNonHeatedWeaponSettings()
    {
        CurrentDamageAmountScaler = DamageScaler;
        CurrentDamageAmountScaler = DamageScaler;

        DamagerComponent.DamageToDeal = CurrentDamageAmountToDeal * CurrentDamageAmountScaler;

    }

    #endregion

    #region Weapon Light Settings

    private void IncreaseWeaponLight()
    {
        WeaponLight.intensity += LightIntensityIncreaseScaler * Time.deltaTime;

        if (WeaponLight.intensity > MaxLightIntensity)
        {
            WeaponLight.intensity = MaxLightIntensity;
        }
    }

    private void DecreaseWeaponLight()
    {
        WeaponLight.intensity -= LightIntensityIncreaseScaler * Time.deltaTime;

        if (WeaponLight.intensity < 0)
        {
            WeaponLight.intensity = 0;
        }
    }


    #endregion


    public void DecreaseDiesel()
    {
        DieselManager.DecreaseDieselByAmount(DieselDrainRate);
    }




}
