using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCollissions : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Proof"))
        {          
            //A�adimos la informaci�n de la prueba encontrada al Game Controller y la eliminamos del mapa
            ProofInfo newInfo = other.transform.GetComponent<ProofBehaviour>().info;
            Debug.Log("Proof found! " + newInfo.proofName);
            GameController.instance.AddProof(newInfo);
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("LightEnemy"))
        {
            //Damos paso a la escena de jumpscare
            transform.GetComponent<PlayerController>().rb.velocity = Vector3.zero;
            GameController.instance.LightEnemyEnding();
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            //Damos paso a la escena de jumpscare, ya que el enemigo nos ha atrapado
            transform.GetComponent<PlayerController>().rb.velocity = Vector3.zero;
            GameController.instance.EnemyEnding();
        }

        if (other.gameObject.CompareTag("Exit"))
        {
            if (GameController.instance.canExit)
            {
                Debug.Log("�Ahora puedes salir por la puerta!");
                other.gameObject.transform.parent.GetComponent<Collider>().isTrigger = true;
            }
            else
            {
                Debug.Log("�Te faltan objetos!");
            }
        }

        if (other.gameObject.CompareTag("WinningSpot") && GameController.instance.canExit)
        {
            Debug.Log("Fin de la partida");
            GameController.instance.EndGame(true);
        }
    }
}
