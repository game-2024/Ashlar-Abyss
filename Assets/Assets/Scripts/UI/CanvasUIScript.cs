using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasUIScript : MonoBehaviour
{
    [SerializeField] private TMP_Text diesel_Text;
    [SerializeField] private TMP_Text health_Text;

    [SerializeField] private DieselManagerScript dieselManager;
    [SerializeField] private PlayerHealthManagerScript healthManager;


    //When object is enable, add Display functions for Diesel and Health as listenrs of the diesel and health managers
    private void OnEnable()
    {
        dieselManager.onDieselUpdated.AddListener(DisplayUpdatedDiesel);
        healthManager.onHealthAdjust.AddListener(DisplayUpdatedHealth);
    }

    //When object is disabled, remove Display functions as listeners to ensure memory is freed
    private void OnDisable()
    {
        dieselManager.onDieselUpdated.RemoveListener(DisplayUpdatedDiesel);
        healthManager.onHealthAdjust.RemoveListener(DisplayUpdatedHealth);
    }

    void Start()
    {
        //Dirty method to have UI display diesel and health at the start.
        //NOTE: Look for better implmentation later

        DisplayUpdatedDiesel(dieselManager.CurrentDiesel, dieselManager.MaxDiesel);

        //Retrieve health for temporary testing, update UI later
        DisplayUpdatedHealth(healthManager.CurrentHealth, healthManager.MaxHealth);
    }

    public void DisplayUpdatedDiesel(int currentDiesel, int maxDiesel)
    {
        diesel_Text.text = "Diesel: " + currentDiesel + " \\ " + maxDiesel;
    }

    public void DisplayUpdatedHealth(int currHealth, int maxHealth)
    {
        health_Text.text = "Health: " + currHealth + " \\ " + maxHealth;
    }

}
