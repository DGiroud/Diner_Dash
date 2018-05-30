using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public GameObject gameController;

    private void OnTriggerEnter(Collider hand)
    {
        
        if (hand.tag == ("EndGame"))
        {                       
            gameController.GetComponent<GameController>().WinScreen();
        }

        if (hand.tag == ("CrunchTime"))
        {
            gameController.GetComponent<GameController>().CrunchTime();
        }


    }
    private void OnTriggerExit(Collider hand)
    {
        Debug.Log("1");
        if (hand.tag == ("CrunchTime"))
        {
            Debug.Log("2");
            gameController.GetComponent<GameController>().NormalSpeed();
        }
    }    
}
