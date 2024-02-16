using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CanvasUIScript : MonoBehaviour
{
    [SerializeField] private TMP_Text diesel_Text;
    [SerializeField] private TMP_Text health_Text;

    // Start is called before the first frame update
    void Start()
    {
        //Temporarly retrive the DieselManager gameobject and get current and max diesel value for displaying

        DieselManagerScript tempRetrievalOfDieselManager = FindObjectOfType<DieselManagerScript>();

        DisplayUpdatedDiesel(tempRetrievalOfDieselManager.CurrentDiesel, tempRetrievalOfDieselManager.MaxDiesel);

        //Retrieve health for temporary testing, update UI later

        healthSystemScript tempRetrievalOfHealthScript = FindObjectOfType<healthSystemScript>();

        DisplayUpdatedHealth(tempRetrievalOfHealthScript.currHealth, tempRetrievalOfHealthScript.maxHealth);
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
