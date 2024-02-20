using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class TestInstantiateScritableObject : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private TestScriptableObject scritableObject;
    private LanternScript myLant;

    void Start()
    {

       scritableObject = Instantiate(scritableObject);
        scritableObject.EditTestData("Making New Data");
        scritableObject.PrintMyTestData();


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {

        Destroy(scritableObject);

        if(scritableObject == null)
        {
            Debug.Log("New ScriptObject was Destoryed");
        }

        Debug.Log("In disable");
    }

}
