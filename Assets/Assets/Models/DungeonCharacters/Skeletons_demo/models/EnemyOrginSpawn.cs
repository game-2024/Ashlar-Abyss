using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOrginSpawn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void OnTriggerEnter(Collider other)
    {

        Debug.Log(other.gameObject.name);
        if(other.TryGetComponent<SkeletonEnemy>(out SkeletonEnemy skeleton))
        {
            skeleton.SkeletonState = SkeletonEnemy.EnemyState.Idle;
            Debug.Log("Current State: " + skeleton.SkeletonState + " compared to: " + SkeletonEnemy.EnemyState.Idle);
        }


    }



}
