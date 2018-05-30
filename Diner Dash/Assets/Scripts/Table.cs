using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Table : MonoBehaviour
{
//--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public List<GameObject> tableNodes;             //Location of the seats
    public List<GameObject> seatedCustomers;        //Customers once they're seated
    public GameObject timer;                        //Timer visual feedback    

    private float seatedTimer;                      //How long the customers have been seated
    private int score;                              //Base Score Multiplier
    private bool seated = false;                    //If the table already has a group on it

    private int timer5 = 1;                         //Timer5
    private int timer4 = 2;                         //Timer4
    private int timer3 = 3;                         //Timer3
    private int timer2 = 4;                         //Timer2
    private int timer1 = 5;                         //Timer1

    private int cC;                                 //Amount of CHILDREN in the UNSEATED group
    private int sC;                                 //Amount of CUSTOMERS in the SEATED group
    private GameObject sG;                          //Shortcut to get the selected group from the gamecontroller
//--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    private void Update()
    {
        if (seated)                                                                                     //Checks whether the table has a group on it
        {
            float multiplier = 1 / (float)sC;                                                           //Sets up the multiplier for the timer (more customers = longer timer
            seatedTimer += Time.deltaTime * multiplier;                                                 //Starts the timer
        }

        if (seatedTimer >= timer5)                                                                      //First timer node
            gameObject.transform.GetChild(0).transform.GetChild(4).gameObject.SetActive(false);         //Turns off

        if (seatedTimer >= timer4)                                                                      //Second timer
            gameObject.transform.GetChild(0).transform.GetChild(3).gameObject.SetActive(false);         //Turns off node

        if (seatedTimer >= timer3)                                                                      //Third timer
            gameObject.transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(false);         //Turns off node

        if (seatedTimer >= timer2)                                                                      //Fourth timer
            gameObject.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);         //Turns off node

        if (seatedTimer >= timer1)                                                                      //Last timer
        {
            timer.gameObject.SetActive(false);                                                          //Sets the parent timer off

            foreach (Transform child in gameObject.transform.GetChild(0))                               //Gets all the timer nodes
            {
                child.gameObject.SetActive(true);                                                       //Turns them back on
            }

            seated = false;                                                                             //Make the table available for the next group
            Camera.main.GetComponent<GameController>().scoreAmount += score;                            //Adds the score of the table to the players total

            foreach (Transform child in transform)                                                      //Finds all of the children
            {
                if (child.tag == "Customer")                                                            //Checks to see what children are customers
                    Destroy(child.gameObject);                                                          //Deletes them
            }

            gameObject.transform.tag = "TableEmpty";                                                    //Sets the tables tag as empty
            seatedCustomers.Clear();                                                                    //Clears the seated cutomer list
            cC = 0;                                                                                     //Resets data to zero
            sC = 0;                                                                                     //Resets data to zero
            seatedTimer = 0;                                                                            //Resets data to zero
            score = 0;                                                                                  //Resets data to zero
        }
    }
//--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    private void OnTriggerEnter(Collider group)
    {
        if (transform.tag == "TableEmpty")
        {
            if (group.tag == "Group")                                                                       //Checks that the collision is a Group
            {
                cC = group.gameObject.transform.childCount;                                                 //Data variable fo the amount of children in the group
                sG = GameObject.Find("GameController").GetComponent<GameController>().selectedGroup;        //Reference to the game controllers selected group


                if (group.GetComponent<Group>().targetTable != GetComponent<BoxCollider>())                 //Checks to see if its the corectly assigned group
                    return;                                                                                 //Reject group

                if (tableNodes.Count < group.GetComponent<Group>().customerList.Count)                      //Checks to see if its the right sized table
                    return;                                                                                 //Reject group

                if (tableNodes.Count >= group.GetComponent<Group>().customerList.Count)                     //Checks to see if its the right sized table
                {
                    if (seated == false)                                                                    //Checks to see if the table already has a group on it
                    {
                        for (int i = 0; i <= cC - 9; i++)                                                   //Gets the amount of customers
                        {
                            group.gameObject.transform.GetChild(8).SetParent(transform);                    //Transfers them over to the table as the new parent
                        }

                        seated = true;                                                                      //Makes it so other groups cant be added to the table

                        Seating();                                                                          //Runs the seating function

                        if (sG == group.gameObject)                                                         //Checks if the selected object in the game controller is STILL the same group
                        {
                            sG = null;                                                                      //Changes the selected object in the game controller to an empty object
                        }
                        Destroy(group.gameObject);                                                          //Destroys the now empty group
                        transform.tag = "TableFull";
                    }
                }
            }
        }
    }
//--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    private void Seating()
    {

        foreach (Transform child in transform)                                                      //Searching the table for children                          
        {
            if (child.tag == "Customer")                                                            //Checks to see what children are customers
                seatedCustomers.Add(child.gameObject);                                              //Populates the customer List
        }

        sC = seatedCustomers.Count;                                                                 //Sets the reference for customers in the seated group

        for (int i = 1; i <= sC; i++)                                                               //Gets the amount of seated customers
        {
            seatedCustomers[i - 1].transform.position = tableNodes[i - 1].transform.position;       //Sets their position to the correct seating arrangement
        }

        Seated();                                                                                   //Runs the seated function
    }
//--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    private void Seated()
    {
        score = (sC * 10);                                              //Sets the score depending on the amount of customers

        gameObject.transform.GetChild(0).gameObject.SetActive(true);    //Turns the visual timer on
    }
//--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
}