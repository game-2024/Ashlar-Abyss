using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LanternScript : MonoBehaviour
{
    [SerializeField] Light lanternLight;
    [SerializeField] float maxLightIntensity;
    [SerializeField] float lightDimmingScale = 0.2f;

    [SerializeField] DieselManagerScript dieselManager;

    [SerializeField]
    private int drainRate;

    [SerializeField] TimeIntervalComponentScript lanternTimer;

    //Custom Material Used to have Sphere fade out when lantern is toggle off.
    //FOR TESTING PURPOSED ONLY
    [SerializeField] Material lanterMaterial;


    private bool playerBrightensLantern = false;


    void Update()
    {
        if(playerBrightensLantern == true && dieselManager.CurrentDiesel > 0)
        {
            BrightenLantern();
            RaiseLanternOpacity();
        }
        else
        {
            TurnOffLantern();
        }
    }


    #region Lantern Adjustment Functions

    //ToggleLantern() Called from Player Controller
    public void ToggleLantern()
    {
        playerBrightensLantern = !playerBrightensLantern;

        if(playerBrightensLantern == true)
        {
            lanternTimer.ResumeTimer();
        }
        else
        {
            lanternTimer.PauseTimer();
        }

    }

    public void TurnOffLantern()
    {
        playerBrightensLantern = false;
        lanternTimer.PauseTimer();
        DimLantern();
        LowerLanterOpacity();

    }

    private void DimLantern()
    {
        lanternLight.intensity -= lightDimmingScale * Time.deltaTime;

        if(lanternLight.intensity < 0)
        {
            lanternLight.intensity = 0;
        }
    }

    private void BrightenLantern()
    {
        lanternLight.intensity += lightDimmingScale * Time.deltaTime;

        if (lanternLight.intensity > maxLightIntensity)
        {
            lanternLight.intensity = maxLightIntensity;
        }

    }

    private void LowerLanterOpacity()
    {
        //Have to create a variable of type color to adjust the alpha of the sphere material
        Color tempColor = lanterMaterial.color;
        tempColor.a -= Time.deltaTime;

        if (tempColor.a < 0)
        {
            tempColor.a = 0;
        }

        lanterMaterial.color = tempColor;
    }

    private void RaiseLanternOpacity()
    {
        //Have to create a variable of type color to adjust the alpha of the sphere material
        Color tempColor = lanterMaterial.color;
        tempColor.a += Time.deltaTime;

        if (tempColor.a > 1)
        {
            tempColor.a = 1;
        }


        lanterMaterial.color = tempColor;
    }

    #endregion

    public void DecreaseDiesel()
    {
        dieselManager.DecreaseDieselByAmount(drainRate);
    }

}
