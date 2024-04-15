using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ColorScript : MonoBehaviour
{
    [SerializeField] Light lanternLight;
    
    // Start is called before the first frame update
    void Start()
    {
         lanternLight.intensity = 1;
    }

    // Update is called once per frame 
    void Update()
    {
        //Gamer light script
        float c = Mathf.Floor(Time.frameCount / 5.0f);
        lanternLight.color = new Color(
           0.5f - 0.5f * Mathf.Sin(c * (float)Mathf.Deg2Rad),
           0.5f - 0.5f * Mathf.Sin((c + 120)  * (float)Mathf.Deg2Rad),
           0.5f - 0.5f * Mathf.Sin((c + 240)  * (float)Mathf.Deg2Rad)
        );
        
    }
}
