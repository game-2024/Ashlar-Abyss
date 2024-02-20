using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "MyTestScriptObject", menuName = "ScriptableObjects/MyTestScriptableObject")]
public class TestScriptableObject : ScriptableObject
{
    [SerializeField] private string myTestData;


    public void EditTestData(string new_data)
    {
        myTestData = new_data;
    }

    public void PrintMyTestData()
    {
        Debug.Log(myTestData);
    }

}
