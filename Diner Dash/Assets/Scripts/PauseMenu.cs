using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour{
//--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    public GameObject pauseMenuUI;          //Reference for the pause menu screen
    public GameObject onScreenUI;           //Reference for the onscreen UI that is on while the game is playing
    public Text score;                      //Reference for the players score

    public bool gameIsPaused = false;       //Lets you know if the game is paused

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public void Resume()
    {
        pauseMenuUI.SetActive(false);       //Turns the pause menu off
        onScreenUI.SetActive(true);         //Turns the on screen UI on
        Time.timeScale = 1f;                //Runs the game at normal speed  
        gameIsPaused = false;               //Sets pause to off
    }
//--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    void Pause()
    {
        pauseMenuUI.SetActive(true);        //Turns the pause menu on
        onScreenUI.SetActive(false);        //Turns the on screen UI off
        Time.timeScale = 0f;                //Pauses the game speed
        gameIsPaused = true;                //Sets pause to on
    }
//--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public void LoadMenu ()
    {
        Time.timeScale = 1f;                //Runs the game at normal speed
        SceneManager.LoadScene("Menu");     //Loads the menu scene
    }
//--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public void LoadGame()
    {
        Time.timeScale = 1f;                //Runs the game at normal speed
        SceneManager.LoadScene("Main");     //Loads the main scene
    }
//--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();                 //Exits the application
    }
//--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    void Update ()
    {
        score.text = GameObject.Find("GameController").GetComponent<GameController>().scoreAmount.ToString();   //Updates the players score

        if (Input.GetKeyDown(KeyCode.Escape))       //Input for the pause menu
        {
            if (gameIsPaused)                       //Checks to see if the game is paused
            {
                Resume();                           //If it isn't, resume the game
            }
            else
            {
                Pause();                            //If it is, pause the game
            }
        }		
	}
//--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
}
