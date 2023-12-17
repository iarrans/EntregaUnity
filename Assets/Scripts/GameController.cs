using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public GameObject Player;

    public List<GameObject> ProofPrefabs;

    public GameObject ProofParent;

    public List<ProofInfo> ProofsFound;

    public float totalTime, currentTime, timeSpent;

    public bool isPlaying, canExit;

    public AudioSource sfx;

    private void Awake()
    {
        currentTime = totalTime;
        timeSpent = 0;
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

        if (SpawningObjectsPLaces.Count> 0)
        {
            int i = Random.Range(0, SpawningObjectsPLaces.Count);
            Player.transform.position = SpawningObjectsPLaces[i].transform.position;
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
            timeSpent++;
            yield return new WaitForSeconds(1);
            }
        }
        Debug.Log("You Lost: Timer Out");
        EndGame(false);
    }

    public void AddProof(ProofInfo info)
    {
        sfx.clip = info.soundEffect;
        sfx.Play();
        ProofsFound.Add(info);
        UIManager.instance.CheckProofImage(info);
        if (ProofsFound.Count == ProofPrefabs.Count)
        {
            canExit = true;
            Debug.Log("¡Ahora puedes huir!");
        }
    }

    public void LightEnemyEnding()
    {
        StopAllCoroutines(); //Para parar cuenta atrás de tiempo
        //Quitamos los controles del jugador
        isPlaying = false;
        StartCoroutine(LightJumpscare());      
    }

    private IEnumerator LightJumpscare()
    {
        //UIManager.instance.SprintSlider.transform.parent.gameObject.SetActive(false);
        UIManager.instance.ScreamerPanel.SetActive(true);
        yield return new WaitForSeconds(2);//Aquí iría el jumpscare
        EndGame(false);
    }

    public void EndGame(bool victory)
    {
        //Pasamos los datos para la siguiente escena
        MainUtils.Victory = victory;
        MainUtils.RemainingSeconds = currentTime;
        MainUtils.objectsCollected = ProofsFound.Count;
        MainUtils.totalObjects = ProofPrefabs.Count;
        MainUtils.TimeSpent = timeSpent;

        //Cargamos escena con resultados

        SceneManager.LoadScene("ResultsScreen");

    }
}
