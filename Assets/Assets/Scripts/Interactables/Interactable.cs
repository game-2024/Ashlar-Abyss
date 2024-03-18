using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{

    public UnityEvent onPerformInteraction;

    //Add PerfomInteraction as a Listener to player's onInteraction Event
    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();

        if (player != null)
        {
            player.onInteractPressed += PerformInteraction;
        }

    }

    //Remove PerfomInteraction as a Listener from player's onInteraction Event
    private void OnTriggerExit(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();

        if (player != null)
        {
            player.onInteractPressed -= PerformInteraction;
        }
    }


    //Invoke Unity Event to perform interaction logic
    public void PerformInteraction()
    {
        onPerformInteraction?.Invoke();
        Debug.Log(gameObject.name + " was interacted with");
    }



}
