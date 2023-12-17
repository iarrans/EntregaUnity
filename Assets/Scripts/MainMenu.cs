using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Runtime.CompilerServices;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update

    public TextMeshProUGUI totalObjectsText;

    public TextMeshProUGUI totalTimeText;

    public TextMeshProUGUI bestTimeText;

    private void Awake()
    {
        //PlayerPrefs.SetFloat("BestTimeEver", 400);
        //PlayerPrefs.SetFloat("HistoryTime", 0);
        if (PlayerPrefs.HasKey("HistoryObjects") && PlayerPrefs.HasKey("HistoryTime") && PlayerPrefs.HasKey("BestTimeEver"))
        {
            totalObjectsText.text = "Total objects collected: " + PlayerPrefs.GetInt("HistoryObjects");
            totalTimeText.text = "Total Time Playing: " + PlayerPrefs.GetFloat("HistoryTime") + " seconds";
            bestTimeText.text = "Best time ever: " + PlayerPrefs.GetFloat("BestTimeEver") + " seconds";
        }
        else
        {
            totalObjectsText.text = "Total objects collected: Play to unlock";
            totalTimeText.text = "Total Time Playing: Play to unlock";
            bestTimeText.text = "Best time ever: Play to unlock";
        }    
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("Map");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
