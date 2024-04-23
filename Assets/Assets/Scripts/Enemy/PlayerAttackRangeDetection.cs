using UnityEngine;

public class PlayerAttackRangeDetection : MonoBehaviour
{

    [SerializeField] SkeletonEnemy Skeleton;



    private void OnTriggerEnter(Collider other)
    {

        if (other.TryGetComponent<PlayerController>(out PlayerController player))
        {
            Skeleton.ChangeState(SkeletonEnemy.EnemyState.Attack);
            Skeleton.PlayerIsInAttackRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            Skeleton.PlayerIsInAttackRange = false;
        }
    }

}
