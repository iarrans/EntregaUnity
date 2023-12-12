using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultsScreenUIManager : MonoBehaviour
{
    //Valores extra�dos del gameplay
    public bool Victory;
    public float RemainingSeconds;
    public int objectsCollected;
    public int totalObjects;

    //Textos de la UI
    public TextMeshProUGUI victoryText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI objectsText;
    public TextMeshProUGUI historyObjects;
    public TextMeshProUGUI historyTime;
    public TextMeshProUGUI besTime;

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

        //Guardamos los datos de la partida en playerprefs

        //Objetos en total
        int previousObjects = 0;
        if (PlayerPrefs.HasKey("HistoryObjects")) previousObjects = PlayerPrefs.GetInt("HistoryObjects");
        int newTotal = previousObjects + totalObjects;
        PlayerPrefs.SetInt("HistoryObjects", previousObjects + totalObjects);
        historyObjects.text = newTotal + " objects collected in total during all trials";

        //Tiempo en total
        float previousTime = 0;
        if (PlayerPrefs.HasKey("HistoryTime")) previousTime = PlayerPrefs.GetFloat("HistoryTime");
        float newTime = previousTime + 240 - RemainingSeconds;
        PlayerPrefs.SetFloat("HistoryTime", newTime); //Sustituir 240 por tiempo total de escena de juego
        historyTime.text = newTime + " in total spent playing this game";

        //Mejor tiempo
        float bestTime = 0;
        if (PlayerPrefs.HasKey("BestTimeEver")) bestTime = PlayerPrefs.GetFloat("BestTimeEver");
        if (RemainingSeconds > bestTime)
        {
            bestTime = RemainingSeconds;
            PlayerPrefs.SetFloat("BestTimeEver", bestTime);
            besTime.text = "Best time ever: " + besTime;
        }  
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void TryAgain()
    {
        SceneManager.LoadScene("Map");
    }
}
