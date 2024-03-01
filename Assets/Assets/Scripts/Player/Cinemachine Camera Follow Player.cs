using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineCameraFollowPlayer : MonoBehaviour
{

    [SerializeField] private GameObject followObject;

    // Update is called once per frame
    void Update()
    {
        float rotationAxis = Input.GetAxis("Mouse X");
    }
}
