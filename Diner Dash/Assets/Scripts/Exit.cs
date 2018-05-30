using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public int scoreMultiplier = 10;
    public int costMultiplier;
    public int score;

    private GameObject cameraSelectedGroup;         //Shortcut to get the selected group from the gamecontroller
    private GameController cameraReference;         //Shortcut to get the game controller script

    private void Awake()
    {
        cameraReference = Camera.main.GetComponent<GameController>();
        cameraSelectedGroup = GameObject.Find("GameController").GetComponent<GameController>().selectedGroup;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Group")
            score = costMultiplier * scoreMultiplier / 4;

        if (other.tag == "Leaving")
            score = costMultiplier * scoreMultiplier / 2;

        if (cameraSelectedGroup == other.gameObject)
            cameraSelectedGroup = cameraReference.emptyGroup;


        costMultiplier = other.GetComponent<Group>().customerList.Count;

        cameraReference.scoreAmount -= score;

        Destroy(other.gameObject);

        if (cameraSelectedGroup == other.gameObject)                                        //Checks if the selected object in the game controller is STILL the same group
        {
            cameraSelectedGroup = null;                //Changes the selected object in the game controller to an empty object
        }
    }




}



