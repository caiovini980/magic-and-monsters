using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScript : MonoBehaviour
{
    public string gameScene;
    public string menuScene;
    public GameObject pausePanel;
    public GameObject gameOverPanel;
    public GameObject UIPanel;
    public GameObject victoryPanel;
    private bool isPaused;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(!isPaused)
            {
                isPaused = true;
                pausePanel.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                isPaused = false;
                pausePanel.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(gameScene);
    }

    public void QuitGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(menuScene);
    }

    public void Die()
    {
        gameOverPanel.SetActive(true);
    }

    public void Victory()
    {
        UIPanel.SetActive(false);
        victoryPanel.SetActive(true);
    }
}
