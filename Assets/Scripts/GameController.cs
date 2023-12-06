using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public List<GameObject> ProofPrefabs;

    public GameObject ProofParent;
    public List<GameObject> InstantiatedProofs;

    public List<ProofInfo> ProofsFound;

    public float totalTime, currentTime;

    public bool isPlaying;

    void Awake()
    {
        instance = this;
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
            InstantiatedProofs.Add(newProof);
            int i = Random.Range(0, SpawningObjectsPLaces.Count);
            newProof.transform.position = SpawningObjectsPLaces[i].transform.position;
            Debug.Log(proof.GetComponent<ProofBehaviour>().info.proofName);
            UIManager.instance.CreateProofButton(proof.GetComponent<ProofBehaviour>().info);
            SpawningObjectsPLaces.Remove(SpawningObjectsPLaces[i]);
        }

        isPlaying = true;
        yield return null;
        StartCoroutine(Timer());
    }

    public IEnumerator Timer()
    {
        while (currentTime > 0)
        {
            if (isPlaying) { 
            currentTime--;
            Debug.Log(currentTime);
            yield return new WaitForSeconds(1);
            }
        }
        Debug.Log("You Lost: Timer Out");
    }

    public void AddProof(ProofInfo info)
    {
        ProofsFound.Add(info);
        UIManager.instance.CheckProofImage(info);
    }
 

}
