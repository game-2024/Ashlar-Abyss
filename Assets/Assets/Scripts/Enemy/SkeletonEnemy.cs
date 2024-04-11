using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonEnemy : MonoBehaviour
{
    [SerializeField] private NavMeshAgent EnemyNavAgent;
    [SerializeField] private Animator EnemyAnimator;

    [SerializeField] private Damager WeaponDamager;

    private PlayerController Player;
    private Vector3 StartingPosition;




    [SerializeField] private float maxHealth;
    public float MaxHealth 
    {
        private set { maxHealth = value; }
        get { return currentHealth; }
    }

    private float currentHealth;
    public float CurrentHealth
    {
        private set {
            
            currentHealth = value;

            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }

            if (currentHealth < 0)
            {
                currentHealth = 0;
                Destroy(this.gameObject);
            }
        }

        get { return currentHealth; }
    }

    public enum EnemyState
    {
        Idle,
        Chase,
        Return,
        Attack
    }
    public EnemyState SkeletonState;



    public void TakeDamage(float damage_to_take)
    {
        Debug.Log("I am hurt pls help");
        CurrentHealth -= damage_to_take;
    }






    // Start is called before the first frame update
    void Start()
    {
        StartingPosition = transform.position;  
        SkeletonState = EnemyState.Idle;
        Player = null;

        WeaponDamager.enabled = false;

        currentHealth = maxHealth;

        StartCoroutine(SkeletonCoroutineUpdate());

    }

    private IEnumerator SkeletonCoroutineUpdate()
    {
        while (true)
        {
            switch (SkeletonState)
            {
                case EnemyState.Idle:
                    IdleUpdate();
                    break;

                case EnemyState.Chase:
                    ChaseUpdate();
                    break;

                case EnemyState.Return:
                    ReturnUpdate();
                    break;

                case EnemyState.Attack:
                    AttackUpdate();
                    yield return StartCoroutine(AttackUpdate());
                    break;

                default:
                    break;

            }


            yield return null;
        }
    }

    private void IdleUpdate()
    {
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
            ChangeState(EnemyState.Idle);
        }

    }

    private IEnumerator AttackUpdate()
    {

        EnemyAnimator.SetTrigger("isAttacking");
        EnemyNavAgent.isStopped = true;
        Debug.Log("Calling wait");
        float animLength = EnemyAnimator.GetCurrentAnimatorStateInfo(0).length;

        WeaponDamager.enabled = true;
        //1f is used to give enough time to have enemy not move and for animation to finish
        yield return new WaitForSeconds(1f);

        Debug.Log("After wait");
        WeaponDamager.enabled = false;

        float distanceToPlayer = Vector3.Distance(Player.transform.position, transform.position);
        if (distanceToPlayer > 2f)
        {
            EnemyNavAgent.isStopped = false;
            SkeletonState = EnemyState.Chase;
        }
        

    }



    public void ChangeState(EnemyState newState)
    {
        switch (newState)
        {
            case EnemyState.Idle:
                EnemyAnimator.SetBool("isMoving", false);
                break;

            case EnemyState.Chase:
                break;

            case EnemyState.Return:
                EnemyNavAgent.SetDestination(StartingPosition);
                EnemyAnimator.SetBool("isMoving", true);

                Player = null;
                break;

            case EnemyState.Attack:
                break;


        }

        SkeletonState = newState;

    }



    public void SetPlayerReference(PlayerController player)
    {
        Player = player;
    }



}
