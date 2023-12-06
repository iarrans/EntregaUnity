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
        GameController.instance = this;
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
