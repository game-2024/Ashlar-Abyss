using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHudDial : MonoBehaviour
{


    public float NeedleSpeed = 1f;

    #region Diesel Fields
    [SerializeField] private DieselManagerScript DieselManager;

    [Serializable]
    private class DieselNeedleFields
    {

        public Image DieselNeedle;

        public float MaximumDieselValueRotation = -187f;
        public float MinimumDieselValueRotation = 11f;

    }

    [SerializeField] private DieselNeedleFields dieselValues;

    [SerializeField] private float DieselPercentage;

    #endregion


    #region Health Fields

    [SerializeField] private PlayerHealthManagerScript PlayerHealthManager;

    [Serializable]
    private class HealthNeedleFields
    {
        public Image HealthNeedle;

        public float MaximumHealthValueRotation = -187f;
        public float MinimumHealthValueRotation = 11f;
    }

    [SerializeField] private HealthNeedleFields healthValues;

    private float HealthPercentage = 1f;

    #endregion

    private void Start()
    {
        DieselPercentage = 0f;
    }


    private void Update()
    {
        #region Diesel Needle Update

        //Variable will be value between Min and Max Value Rotation based of percantage(lerp) between Max and Min
        float dialValueToRotateTo = Mathf.Lerp(dieselValues.MinimumDieselValueRotation,dieselValues.MaximumDieselValueRotation, DieselPercentage);


        Quaternion currentRotation = dieselValues.DieselNeedle.transform.rotation;
        Quaternion newRotation = Quaternion.Euler(new Vector3(0f, 0f, dialValueToRotateTo));

        dieselValues.DieselNeedle.transform.rotation = Quaternion.Lerp(currentRotation, newRotation, Time.deltaTime);

        #endregion


        #region Health Needle Update


        //Variable will be value between Min and Max Value Rotation based of percantage(lerp) between Max and Min
        float healthDialValueToRotateTo = Mathf.Lerp(healthValues.MinimumHealthValueRotation,
                                    healthValues.MaximumHealthValueRotation, HealthPercentage);

        Quaternion healthCurrentRotation = healthValues.HealthNeedle.transform.rotation;
        Quaternion healthNewRotation = Quaternion.Euler(new Vector3(0f, 0f, healthDialValueToRotateTo));

        healthValues.HealthNeedle.transform.rotation = Quaternion.Lerp(healthCurrentRotation, healthNewRotation, Time.deltaTime);



        #endregion



        if(Input.GetKeyDown(KeyCode.O))
        {
            PlayerHealthManager.DamagePlayer(10f);
        }


    }

    //Function is Listner to the DieselManager Scriptable Object
    private void UpdateDiesel(int currentNewDiesel, int maxNewDiesel)
    {
        if (maxNewDiesel == 0)
        {
            DieselPercentage = 0f;
        }
        else
        {
            //DieselPercentage = Mathf.Clamp(currentNewDiesel / (float)maxNewDiesel, 0f, 1f);
            DieselPercentage = currentNewDiesel / 100f;
        }
    }

    //Function is Listner to the PLayerHealthManager Scriptable Object
    private void UpdateHealth(float currentNewHealth, float maxNewHealth)
    {
        HealthPercentage = Mathf.Clamp(currentNewHealth / maxNewHealth,0f,1f);
    }

    private void OnEnable()
    {
        DieselManager.onDieselUpdated.AddListener(UpdateDiesel);
        PlayerHealthManager.onHealthAdjust.AddListener(UpdateHealth);
    }

    private void OnDisable()
    {
        DieselManager.onDieselUpdated.RemoveListener(UpdateDiesel);
        PlayerHealthManager.onHealthAdjust.RemoveListener(UpdateHealth);
    }


}
