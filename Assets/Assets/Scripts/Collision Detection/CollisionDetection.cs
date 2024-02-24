using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionDetection : MonoBehaviour
{
    [SerializeField] private bool isTimerActivated = false;

    private bool timeIntervalReached = false;

    [SerializeField] private UnityEvent<Collider> onPerformActionWhenTriggerEntryOrTick;
    [SerializeField] private UnityEvent onEntityEnterCollision;
    [SerializeField] private UnityEvent onEntityExitCollision;



    private void OnTriggerEnter(Collider other)
    {
        onEntityEnterCollision?.Invoke();

        if(isTimerActivated == false)
        {
           onPerformActionWhenTriggerEntryOrTick?.Invoke(other);
        }

    }

    private void OnTriggerStay(Collider other)
    {
        
        if(timeIntervalReached == false || isTimerActivated == false) { return; }

        //Currently Directly Looks for Player. Later edit to work with enemies if needed
        //PlayerStatsScript player = other.GetComponent<PlayerStatsScript>();

        //Perform Action Event if there is a Player
        //if (player != null)
        //{
            onPerformActionWhenTriggerEntryOrTick?.Invoke(other);
        //}

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
