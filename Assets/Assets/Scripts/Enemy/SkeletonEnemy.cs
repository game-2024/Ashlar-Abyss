using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonEnemy : MonoBehaviour
{
    [SerializeField] private NavMeshAgent EnemyNavAgent;
    [SerializeField] private Animator EnemyAnimator;


    [SerializeField] private PlayerController Player;

    private bool PlayerFound = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        if(PlayerFound == true)
        {
            EnemyNavAgent.SetDestination(Player.transform.position);
            EnemyAnimator.SetBool("isMoving", true);
        }


    }




    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<PlayerController>(out PlayerController player))
        {
            Player = player;
            PlayerFound = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if( other.GetComponent<PlayerController>() != null )
        {
            Player = null;
            PlayerFound = false;
            EnemyAnimator.SetBool("isMoving", false);
        }
    }



}
