using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent agent = null;
    [SerializeField]private Transform target;
    void GetReferences()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    void MoveToTarget()
    {
        agent.SetDestination(target.position);
    }
    void RotateToTarget()
    {
        transform.LookAt(target.position);
    }
    
    void Start()
    {
        GetReferences();

    }
  

    
    void Update()
    {
        MoveToTarget();
       RotateToTarget();
    }
}
