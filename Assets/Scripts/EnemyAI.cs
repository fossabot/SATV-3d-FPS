﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;
    NavMeshAgent navMeshAgent;
     [SerializeField] float chaseRange = 300f;
    [SerializeField] float turnSpeed = 5f;
    float distanceToTarget = Mathf.Infinity;
    bool isProvoked = false;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        distanceToTarget = Vector3.Distance(target.position, transform.position);

        if(isProvoked)
        {
            EngageTarget();
        }
        else if(distanceToTarget<= chaseRange)
        {
            isProvoked = true;
        }


       /* if (distanceToTarget <= chaseRange)
        {
            navMeshAgent.SetDestination(target.position);
        }*/

    }

    private void EngageTarget()
    {
        FaceTarget();
        if(distanceToTarget >= navMeshAgent.stoppingDistance)
        {
           
            ChaseTarget();
        }

       /* if(distanceToTarget <= navMeshAgent.stoppingDistance)
        {
            AttackTarget();
        }*/
    }

    private void ChaseTarget()
    {
         //GetComponent<Animator>().SetBool("attack", false);

         //GetComponent<Animator>().SetTrigger("move");
        //GetComponent<Animator>().SetTrigger("move");
        navMeshAgent.SetDestination(target.position);
    }

   
    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);

    }

    void OnDrawGizmosSelected()
    {
        // Display the explosion radius when selected
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
    public void OnDamage()
    {
        isProvoked = true;
    }
}
