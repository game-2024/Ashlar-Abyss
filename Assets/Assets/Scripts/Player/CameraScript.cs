using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.WebCam;

public class CameraScript : MonoBehaviour
{
    // Start is called before the first frame update
    private Camera cam;

    [SerializeField] private PlayerStatsScript player;
    [SerializeField] private float camSpeed;


    public float y_flip;

    private Vector3 playerLastPosition;

    Quaternion holdQuant;

    void Start()
    {
        cam = GetComponent<Camera>();
        playerLastPosition = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        float tempY = Input.GetAxis("Mouse X") * y_flip;
        float tempX = Input.GetAxis("Mouse Y") * -1f;



        //transform.RotateAround(player.transform.position, Vector3.up, tempY);
        //transform.RotateAround(player.transform.position, Vector3.right, tempX);


        //transform.rotation = Quaternion.AngleAxis(tempY, Vector3.up) * transform.rotation;

        transform.position = transform.position - (playerLastPosition - player.transform.position);

        playerLastPosition = player.transform.position;

        transform.RotateAround(player.transform.position, Vector3.up, tempY);
        transform.RotateAround(player.transform.position, Vector3.right, tempX);

        Vector3 angle = transform.rotation.eulerAngles;
        angle.z = 0f;

        transform.rotation = Quaternion.Euler(angle);

        //transform.eulerAngles = cam.transform.eulerAngles + new Vector3(0, tempY, 0);

    }


}
