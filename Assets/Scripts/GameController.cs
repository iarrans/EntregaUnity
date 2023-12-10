using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public List<GameObject> ProofPrefabs;

    public GameObject ProofParent;

    public List<ProofInfo> ProofsFound;

    public float totalTime, currentTime;

    public bool isPlaying, canExit;

    private void Awake()
    {
        currentTime = totalTime;
    }

    void Start()
    {
        instance = this;
        canExit = false;
        isPlaying = false;
        StartCoroutine(LoadGame());
    }

    public IEnumerator LoadGame()
    {
        List<GameObject> SpawningObjectsPLaces = GameObject.FindGameObjectsWithTag("ProofSpot").ToList<GameObject>();
        ProofsFound = new List<ProofInfo>();
        currentTime = totalTime;
        foreach (GameObject proof in ProofPrefabs)
        {
            GameObject newProof = Instantiate(proof, ProofParent.transform);
            int i = Random.Range(0, SpawningObjectsPLaces.Count);
            newProof.transform.position = SpawningObjectsPLaces[i].transform.position;
            Debug.Log(proof.GetComponent<ProofBehaviour>().info.proofName);
            UIManager.instance.CreateProofButton(proof.GetComponent<ProofBehaviour>().info);
            SpawningObjectsPLaces.Remove(SpawningObjectsPLaces[i]);
        }

        isPlaying = true;
        yield return null;
        //Solo se empieza la cuenta atrás una vez se han terminado de cargar y posicionar los recursos del juego
        StartCoroutine(Timer());
    }

    public IEnumerator Timer()
    {
        while (currentTime > 0)
        {
            if (isPlaying) { 
            currentTime--;
            yield return new WaitForSeconds(1);
            }
        }
        Debug.Log("You Lost: Timer Out");
        EndGame(false);
    }

    public void AddProof(ProofInfo info)
    {
        ProofsFound.Add(info);
        UIManager.instance.CheckProofImage(info);
        if (ProofsFound.Count == ProofPrefabs.Count)
        {
            canExit = true;
            Debug.Log("¡Ahora puedes huir!");
        }
    }

    public void EndGame(bool victory)
    {
        //Pasamos los datos para la siguiente escena
        MainUtils.Victory = victory;
        MainUtils.RemainingSeconds = currentTime;
        MainUtils.objectsCollected = ProofsFound.Count;
        MainUtils.totalObjects = ProofPrefabs.Count;

        //Cargamos escena con resultados

        SceneManager.LoadScene("ResultsScreen");

    }

}
