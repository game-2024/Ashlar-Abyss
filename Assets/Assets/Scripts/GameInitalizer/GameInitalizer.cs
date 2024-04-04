using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitalizer : MonoBehaviour
{
    [SerializeField] DieselManagerScript DieselManager;


    void Start()
    {
        DieselManager.InitialDieselValuesInvoke();

        Destroy(this.gameObject);
    }

}
