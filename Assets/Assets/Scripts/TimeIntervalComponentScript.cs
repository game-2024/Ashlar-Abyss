using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;



public class TimeIntervalComponentScript : MonoBehaviour
{

    [SerializeField] [Range(0.1f,20)]
    [Tooltip ("Counted in Seconds")]
        private float timePerInterval = 1f;

    private float elapsed_Time = 0;

    [SerializeField] private bool timerPaused = false;

    [SerializeField] private UnityEvent onTimerComplete;


    void Update()
    {
        if(timerPaused == true)
        {
            return;
        }

        elapsed_Time += Time.deltaTime;

        if(elapsed_Time >= timePerInterval)
        {
            ResetTimer();

            onTimerComplete?.Invoke();
        }
    }



    public void PauseTimer()
    {
        timerPaused = true;
    }

    public void ResumeTimer()
    {
        timerPaused = false;
    }

    public void TogglePauseTimer()
    {
        timerPaused = !timerPaused;
    }

    public void ResetTimer()
    {
        elapsed_Time = 0f;
    }

}