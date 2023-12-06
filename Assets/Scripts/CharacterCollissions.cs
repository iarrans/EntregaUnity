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
            //Añadimos la información de la prueba encontrada al Game Controller y la eliminamos del mapa
            ProofInfo newInfo = other.transform.GetComponent<ProofBehaviour>().info;
            Debug.Log("Proof found! " + newInfo.proofName);
            GameController.instance.ProofsFound.Add(newInfo);
            Destroy(other.gameObject);
        }
    }
}
