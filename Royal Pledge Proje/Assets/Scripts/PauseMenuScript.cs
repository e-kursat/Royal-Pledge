using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    public GameObject inGameScreen, pauseScreen;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            inGameScreen.SetActive(false);
            pauseScreen.SetActive(true);
        }
    }

    public void PauseButton()
    {
        Time.timeScale = 0;
        inGameScreen.SetActive(false);
        pauseScreen.SetActive(true);

    }

    public void PlayButton()
    {
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
        inGameScreen.SetActive(true);
        

    }

    public void ReplayButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }

    public void HomeButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);

    }
    
}


