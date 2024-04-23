using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{

    public UnityEvent onPerformInteraction;

    PlayerController player = null;

    //Invoke Unity Event to perform interaction logic
    public void PerformInteraction()
    {
        onPerformInteraction?.Invoke();
        //Debug.Log(gameObject.name + " was interacted with");
    }

    //Add PerfomInteraction as a Listener to player's onInteraction Event
    private void OnTriggerEnter(Collider other)
    {
        player = other.GetComponent<PlayerController>();

        if (player != null)
        {
            //Debug.Log("Entered interataction zone");
            player.onInteractPressed += PerformInteraction;
        }

    }


    //Remove PerfomInteraction as a Listener from player's onInteraction Event
    private void OnTriggerExit(Collider other)
    {
        player = other.GetComponent<PlayerController>();

        if (player != null)
        {
            //Debug.Log("Exit interataction zone");
            player.onInteractPressed -= PerformInteraction;
        }
    }


    private void OnDestroy()
    {
        if(player != null)
        {
            player.onInteractPressed -= PerformInteraction;
        }
    }




}
