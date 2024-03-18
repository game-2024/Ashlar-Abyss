using UnityEngine;
using UnityEngine.UI;

public class DieselDial : MonoBehaviour
{

    [SerializeField] private Image Needle;
    [SerializeField] private DieselManagerScript DieselManager;

    [SerializeField] private float MaximumDieselValueRotation = -90f;
    [SerializeField] private float MinimumDieslValueRotation = 90f;

    private float OldDieselValueRotation;
    private float NewDieselValueRotation;

    [SerializeField][Range(0f,1f)] private float DieselPercentage;

    public void UpdateDiesel(int currentNewDiesel, int maxNewDiesel)
    {
        OldDieselValueRotation = DieselPercentage;
        DieselPercentage = currentNewDiesel/ (float)maxNewDiesel;
    }

    private void Update()
    {
        float dialRotation = Mathf.Lerp(MinimumDieslValueRotation, MaximumDieselValueRotation, DieselPercentage);


        Quaternion currentRotation = Needle.transform.rotation;

        Quaternion newRotation = Quaternion.Euler(new Vector3(0f, 0f, dialRotation));

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
