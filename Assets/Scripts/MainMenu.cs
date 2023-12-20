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

    public AudioClip buttonEffect;
    public AudioSource SFX;

    private void Awake()
    {
            totalObjectsText.text = "Total objects collected: " + PlayerPrefs.GetInt("HistoryObjects");
            totalTimeText.text = "Total Time Playing: " + PlayerPrefs.GetFloat("HistoryTime") + " seconds";
            bestTimeText.text = "Best time ever: " + PlayerPrefs.GetFloat("BestTimeEver") + " seconds";   
    }

    public void PlayButtonSound()
    {
        SFX.clip = buttonEffect;
        SFX.Play();
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
