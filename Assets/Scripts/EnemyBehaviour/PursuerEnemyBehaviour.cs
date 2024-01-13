using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PursuerEnemyBehaviour : MonoBehaviour
{
    //Enemigo básico que persigue al jugador siempre, muy lentamente.
    NavMeshAgent agent;
    [SerializeField] private Transform playerTransform;
    public AudioClip screamer;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Start()
    {
        agent.SetDestination(playerTransform.position);
        StartCoroutine(UpdateDestination());
    }

    private IEnumerator UpdateDestination()
    {
        while (GameController.instance.currentTime > 0)
        {
            //Debug.Log("Agent repath");

            agent.SetDestination(playerTransform.position);

            NavMeshPath path = new NavMeshPath();

            agent.CalculatePath(playerTransform.position, path);

            yield return new WaitForSeconds(3);
        }   
    }

}
