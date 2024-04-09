using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonEnemy : MonoBehaviour
{
    [SerializeField] private NavMeshAgent EnemyNavAgent;
    [SerializeField] private Animator EnemyAnimator;


    private PlayerController Player;


    private Vector3 StartingPosition;


    public enum EnemyState
    {
        Idle,
        Chase,
        Return,
        Attack
    }
    public EnemyState SkeletonState;






    // Start is called before the first frame update
    void Start()
    {
        StartingPosition = transform.position;  
        SkeletonState = EnemyState.Idle;
        Player = null;
    }

    // Update is called once per frame
    void Update()
    {


        switch (SkeletonState)
        {
            case EnemyState.Idle:
                IdleUpdate();
                break;

            case EnemyState.Chase:
                ChaseUpdate();
                break ;

            case EnemyState.Return:
                ReturnUpdate();
                break;

            case EnemyState.Attack:
                StartCoroutine(AttackUpdate());
                break;

            default:
                break;

                    
        }


    }



    private void IdleUpdate()
    {
        EnemyAnimator.SetBool("isMoving", false);
    }


    private void ChaseUpdate()
    {

        if(Player == null)
        {
            SkeletonState = EnemyState.Return;
            return;
        }



        Vector3 playerPosition = Player.transform.position;
        playerPosition.y = 0f;

        EnemyNavAgent.SetDestination(playerPosition);
        EnemyAnimator.SetBool("isMoving", true);


        float distanceToPlayer = Vector3.Distance(Player.transform.position, transform.position);

        if(distanceToPlayer < 2f)
        {
            SkeletonState = EnemyState.Attack;
        }


    }

    private void ReturnUpdate()
    {

        float distance = Vector3.Distance(StartingPosition,transform.position);

        if (distance < 0.5f)
        {
            Debug.Log("position reached");
            SkeletonState = EnemyState.Idle;
        }

    }

    private IEnumerator AttackUpdate()
    {

        EnemyAnimator.Play("DS_onehand_attack_A");


        EnemyNavAgent.isStopped = true;

        Debug.Log("Calling wait");


        float animLength = EnemyAnimator.GetCurrentAnimatorStateInfo(0).length;


        yield return new WaitForSeconds(animLength);
        Debug.Log("After wait");

        EnemyNavAgent.isStopped = false;

        SkeletonState = EnemyState.Chase;


    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<PlayerController>(out PlayerController player))
        {
            //Set State to Chase Player

            SkeletonState = EnemyState.Chase;
            Player = player;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {

            //Set State to Return

            SkeletonState = EnemyState.Return;

            EnemyNavAgent.SetDestination(StartingPosition);
            EnemyAnimator.SetBool("isMoving", true);

            Player = null;

        }
    }



}
