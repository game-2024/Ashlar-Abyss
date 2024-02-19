using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionDetection : MonoBehaviour
{
    [SerializeField] private bool isTimerTickActive = false;

    private bool timeIntervalReached = false;

    [SerializeField] private UnityEvent onPerformActionWhenTriggerEntryOrTick;
    [SerializeField] private UnityEvent onEntityEnterCollision;
    [SerializeField] private UnityEvent onEntityExitCollision;



    private void OnTriggerEnter(Collider other)
    {
        onEntityEnterCollision?.Invoke();

        if(isTimerTickActive == false)
        {
            //Currently Directly Looks for Player. Later edit to work with enemies if needed
            PlayerStatsScript player = other.GetComponent<PlayerStatsScript>();

            //Perform Action Event if there is a Player
            if (player != null)
            {
                onPerformActionWhenTriggerEntryOrTick?.Invoke();
            }

        }

    }

    private void OnTriggerStay(Collider other)
    {
        
        if(timeIntervalReached == false || isTimerTickActive == false) { return; }

        //Currently Directly Looks for Player. Later edit to work with enemies if needed
        PlayerStatsScript player = other.GetComponent<PlayerStatsScript>();

        //Perform Action Event if there is a Player
        if (player != null)
        {
            onPerformActionWhenTriggerEntryOrTick?.Invoke();
        }

        timeIntervalReached = false;

    }

    private void OnTriggerExit(Collider other)
    {
        onEntityExitCollision?.Invoke();
    }


    public void TimeIntervalReached()
    {
        timeIntervalReached = true;
    }




}
