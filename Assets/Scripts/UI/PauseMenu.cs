using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool isPaused = false;

    public GameObject pausePanel;

    public void PauseGame()
    {
        if (!isPaused)
        {
            pausePanel.SetActive(true);
            isPaused = true;
            GameController.instance.isPlaying = false;
            Time.timeScale = 0;
        }
        Debug.Log("Game has been paused");
    }


    public void ContinueGame()
    {
        pausePanel.SetActive(false);
        isPaused = false;
        GameController.instance.isPlaying = true;
        Time.timeScale = 1;
        Debug.Log("Game has been restored");
    }

    public void ReloadGame()
    {
        Time.timeScale = 1;
        int indexScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(indexScene);
        Debug.Log("Game has been reloaded");
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
        Debug.Log("Main Menu");
    }

}
