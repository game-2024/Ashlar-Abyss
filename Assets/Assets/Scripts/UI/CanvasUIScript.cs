using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CanvasUIScript : MonoBehaviour
{
    [SerializeField] private TMP_Text textField;

    // Start is called before the first frame update
    void Start()
    {
        //Temporarly retrive the DieselManager gameobject and get current and max diesel value for displaying

        DieselManagerScript tempRetrivealOfDieselManager = FindObjectOfType<DieselManagerScript>();

        DisplayUpdatedDiesel(tempRetrivealOfDieselManager.CurrentDiesel, tempRetrivealOfDieselManager.MaxDiesel);
    }

    public void DisplayUpdatedDiesel(int currentDiesel, int maxDiesel)
    {
        textField.text = "Diesel: " + currentDiesel + " \\ " + maxDiesel;
    }

}
