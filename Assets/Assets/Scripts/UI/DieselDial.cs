using UnityEngine;
using UnityEngine.UI;

public class DieselDial : MonoBehaviour
{

    [SerializeField] private Image Needle;
    [SerializeField] private DieselManagerScript DieselManager;

    [SerializeField] private float MaximumDieselValueRotation = -90f;
    [SerializeField] private float MinimumDieslValueRotation = 90f;

    private float DieselPercentage = 1f;

    private void UpdateDiesel(int currentNewDiesel, int maxNewDiesel)
    {
        DieselPercentage = Mathf.Clamp(currentNewDiesel/ (float)maxNewDiesel,0f,1f) ;
    }

    private void Update()
    {
        //Variable will be value between Min and Max Value Rotation based of percantage(lerp) between Max and Min
        float dialValueToRotateTo = Mathf.Lerp(MinimumDieslValueRotation, MaximumDieselValueRotation, DieselPercentage);


        Quaternion currentRotation = Needle.transform.rotation;
        Quaternion newRotation = Quaternion.Euler(new Vector3(0f, 0f, dialValueToRotateTo));

        Needle.transform.rotation = Quaternion.Lerp(currentRotation, newRotation, Time.deltaTime);
    }


    private void OnEnable()
    {
        DieselManager.onDieselUpdated.AddListener(UpdateDiesel);
    }

    private void OnDisable()
    {
        DieselManager.onDieselUpdated.RemoveListener(UpdateDiesel);
    }


}
