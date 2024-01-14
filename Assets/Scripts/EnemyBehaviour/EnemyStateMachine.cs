using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : MonoBehaviour
{
    public enum State { patrulla, perseguir, dormido}

    public State initialState;

    public State actualState;

    public List<Transform> wayPoints;

    public int indexNextWaypoint;

    public float minDistance;

    public float range;

    NavMeshAgent agent;

    public Transform playerTransform;

    public int proofsToAwake; //pruebas necesarias para que el enemigo se "despierte" y patrulle

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
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

                agent.SetDestination(playerTransform.position);

                if (Vector3.Distance(transform.position, playerTransform.position) > range)
                {
                    actualState = State.patrulla;
                    //Sustituir, para que sean 3 estados, conque se queda dos segundos a la espera pegando gritos o algo y, después, mandar de vuelta a patrulla
                }
                break;

            case State.dormido://El enemigo no está activo hasta que el jugador no obtiene x objetos
                if (GameController.instance.ProofsFound.Count >= proofsToAwake)
                {
                    Debug.Log("Proofs found: " + GameController.instance.ProofsFound.Count);
                    actualState = State.perseguir;
                }
                break;
        }
    }
}
