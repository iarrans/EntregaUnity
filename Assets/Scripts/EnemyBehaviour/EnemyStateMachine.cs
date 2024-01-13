using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : MonoBehaviour
{
    public enum State { patrulla, perseguir, volver}

    public State initialState;

    public State actualState;

    public List<Transform> wayPoints;

    public int indexNextWaypoint;

    public float minDistance = 1.5f;

    public float range = 5;

    NavMeshAgent agent;

    public Transform playerTransform;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        actualState = initialState;
    }

    private void Update()
    {
        switch (actualState)
        {
            case State.patrulla:

                if (Vector3.Distance(transform.position, wayPoints[indexNextWaypoint].position) < minDistance)
                {
                    indexNextWaypoint++;
                    if (indexNextWaypoint >= wayPoints.Count)
                    {
                        indexNextWaypoint = 0;
                    }               
                }

                agent.SetDestination(wayPoints[indexNextWaypoint].position);

                if (Vector3.Distance(transform.position, playerTransform.position) < range)
                {
                    actualState = State.perseguir;
                }
                break;
                
            case State.perseguir:

                agent.SetDestination(wayPoints[indexNextWaypoint].position);

                if (Vector3.Distance(transform.position, playerTransform.position) > range)
                {
                    actualState = State.patrulla;
                    //Sustituir, para que sean 3 estados, conque se queda dos segundos a la espera pegando gritos o algo y, después, mandar de vuelta a patrulla
                }
                break;

            case State.volver://sustituible por esperar, como cuando te sales del rango de los enemigos de ds, que se quedan un rato tolais antes de volver a us pos.
                //También, se puede hacer que tengan comportamientos más agresivos o hagan algunas acciones específicas si queda poco tiempo
                //También se puede hacer un comportamiento para el jumpscare
                agent.SetDestination(wayPoints[indexNextWaypoint].position);
                break;
        }
    }
}
