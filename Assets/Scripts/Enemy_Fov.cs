using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.AnimatedValues;
using UnityEngine;

using UnityEngine.AI;
using UnityEngine.UIElements;

public class FieldOfView : MonoBehaviour
{
    public GameObject retopovideojuego;
    public Transform target;
    NavMeshAgent nav;
    public float radius;
    [Range(0, 360)]
    public float angle, Watcher;

    public GameObject playerRef;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeePlayer;
    public Animator animator;
   

    private void Start()
    {
        nav=GetComponent<NavMeshAgent>();
        playerRef = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine());
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                    {
                    canSeePlayer = true;
                    transform.GetChild(0).GetComponent<Animator>().SetFloat("Watcher", 1.0f);
                }
                else
                    canSeePlayer = false;
               //salto al jugador
                if (canSeePlayer == true)
                
                {
                   
                    nav.SetDestination(target.position);
                }

                


            }
            else
                canSeePlayer = false;
        }
        else if (canSeePlayer)
            canSeePlayer = false;
    }
}