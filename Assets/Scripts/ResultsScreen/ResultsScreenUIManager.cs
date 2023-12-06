using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultsScreenUIManager : MonoBehaviour
{
    //Valores extraídos del gameplay
    public bool Victory;
    public float RemainingSeconds;
    public int objectsCollected;
    public int totalObjects;

    //Textos de la UI
    public TextMeshProUGUI victoryText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI objectsText;

    private void Awake()
    {
        Victory = MainUtils.Victory;
        RemainingSeconds = MainUtils.RemainingSeconds;
        objectsCollected = MainUtils.objectsCollected;
        totalObjects = MainUtils.totalObjects;
    }

    void Start()
    {
        if (Victory)
        {
            victoryText.text = "Escaped the ritual safely!";
            timeText.text = "You had " + RemainingSeconds + " seconds left, well done.";
            objectsText.text = objectsCollected + "/" + totalObjects + " were collected";
        }
        else
        {
            victoryText.text = "Welcome to the secta's family...";
            if(RemainingSeconds > 0) timeText.text = "You had " + RemainingSeconds + " seconds left, though.";
            else timeText.text = "Runned out of time! Sikes!";
            objectsText.text = objectsCollected + "/" + totalObjects + " were collected";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
