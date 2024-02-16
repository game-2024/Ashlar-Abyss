using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LanternScript : MonoBehaviour, IDieselChangeable
{
    [SerializeField] Light lanternLight;
    [SerializeField] float maxLightIntensity;
    [SerializeField] float lightDimmingScale = 0.2f;

    [SerializeField] DieselManagerScript dieselManager;

    [SerializeField]
    private int drainRate;
    public int DieselDrainRate
    {
        get { return drainRate; }

        set { drainRate = value; }
    }

    [SerializeField] TimeIntervalComponentScript lanternTimer;
    [SerializeField] Material lanterMaterial;


    private bool playerBrightensLantern = false;

    #region
    // Update is called once per frame
    void Update()
    {
        if(playerBrightensLantern && dieselManager.CurrentDiesel > 0)
        {
            BrightenLantern();
            RaiseLanternOpacity();
        }
        else
        {
            DimLantern();
            LowerLanterOpacity();
        }
    }


    //Called from Player Controller
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
        dieselManager.DecreaseDieselByAmount(DieselDrainRate);
    }

    public void IncreaseDiesel()
    {

    }
}
