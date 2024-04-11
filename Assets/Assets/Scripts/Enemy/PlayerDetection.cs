using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{

    [SerializeField] SkeletonEnemy Skeleton;



    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerController>(out PlayerController player))
        {

            Skeleton.ChangeState(SkeletonEnemy.EnemyState.Chase);
            Skeleton.SetPlayerReference(player);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            Skeleton.ChangeState(SkeletonEnemy.EnemyState.Return);
        }
    }
}
