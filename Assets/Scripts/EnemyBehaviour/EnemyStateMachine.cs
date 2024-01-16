using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : MonoBehaviour
{
    public enum State { patrulla, perseguir, dormido, despertar}

    public State initialState;

    public State actualState;

    public List<Transform> wayPoints;

    public int indexNextWaypoint;

    public float minDistance;

    public float range;

    NavMeshAgent agent;

    public float SecondsToAwake = 4;
    public bool isAwake = false;

    public Transform playerTransform;

    public int proofsToAwake; //pruebas necesarias para que el enemigo se "despierte" y patrulle

    public AudioSource audio;

    public List<AudioClip> clipList;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        audio = GetComponent<AudioSource>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        actualState = initialState;
        //Si está "dormido" de inicio, el enemigo estará rezando en bucle
        if (actualState == State.dormido || initialState == State.dormido)
        {
            audio.loop = true;
            audio.clip = clipList[0];
            audio.Play();
        }
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
                    audio.Stop();
                    audio.clip = clipList[1];
                    audio.Play();
                }
                break;
                
            case State.perseguir:
                agent.SetDestination(playerTransform.position);

                if (Vector3.Distance(transform.position, playerTransform.position) > range)
                {
                    actualState = State.patrulla;
                    audio.Stop();
                    audio.clip = clipList[2];
                    audio.Play();
                }
                break;

            case State.dormido://El enemigo no está activo hasta que el jugador no obtiene x objetos. Como si estuviera rezando
                if (GameController.instance.ProofsFound.Count >= proofsToAwake)
                {
                    audio.loop = false;
                    audio.Stop();
                    audio.clip = clipList[3];
                    audio.Play();
                    actualState = State.despertar;
                    StartCoroutine(AwakeCharacter());
                }
                break;

            case State.despertar://Estado transición para no comenzar a perseguir automáticamente al jugador, pero tampoco estar rezando inactivamente. Animación de despertar.
                if (isAwake)
                {
                    actualState = State.patrulla;
                }
            break;
        }
    }

    private IEnumerator AwakeCharacter()
    {
        yield return new WaitForSeconds(SecondsToAwake);
        isAwake = true;
    }

}
