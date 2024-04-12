using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Generator : MonoBehaviour
{
    public UnityEvent onToggleLights;
    private void Start()
    {
    }

    public void LightsOn()
    {
        onToggleLights.Invoke();
    }
}
