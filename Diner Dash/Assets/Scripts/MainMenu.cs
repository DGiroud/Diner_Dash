using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour{
//--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------   
    public void LoadMain()                  //Function
    {
        Time.timeScale = 1f;                //Makes sure time is running normally
        SceneManager.LoadScene("Main");     //Loads the main level
    }
//--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
}
