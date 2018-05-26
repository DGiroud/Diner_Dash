using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Group : MonoBehaviour{
//--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    public List<GameObject> customers;                                  //Lis of all the different customers it can spawn
    public List<GameObject> customerList = new List<GameObject>();      //List Of customers attached to the group
    public List<GameObject> groupNodes = new List<GameObject>();        //Location nodes for the customer placement
    public GameObject marker;                                           //Visual feedback on what group is currently selected
    public GameObject timer;                                            //Visual feedback for how long the group has been waiting
    public bool waiting = false;                                        //Whether or not the group is waiting for a table

    private float waitTime;                                             //The Timer for how long the group has been waiting
    private int timer5 = 1;                                             //Visual feedback for how long the customer will wait
    private int timer4 = 2;                                             //Visual feedback for how long the customer will wait
    private int timer3 = 3;                                             //Visual feedback for how long the customer will wait
    private int timer2 = 4;                                             //Visual feedback for how long the customer will wait
    private int timer1 = 6;                                             //How long the customer will wait

    public BoxCollider targetTable;                                     //The Groups Destination     
    public GameObject exit;                                             //Reference for the exit

    private int customerAmount;                                         //The ACTUAL size of the group
    private int customerRange;                                          //Reference for the group size
    private int customerGenerator;                                      //Reference for the customer prefab generator

    public int groupSize2;                                              //Percentage chance of the group size being 2
    public int groupSize4;                                              //Percentage chance of the group size being 4
    public int groupSize6;                                              //Percentage chance of the group size being 6    
    
    NavMeshAgent agent;                                                 //The NavMesh Reference

//--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    private void Awake()
    {
        customerRange = (Random.Range(1, 101));     //Makes a random number for reference

        if (customerRange <= groupSize2)            //Checks what sized group its going to be
            customerAmount = 2;                     //Set the group size to 2

        else if (customerRange <= groupSize4)       //Checks what sized group its going to be
            customerAmount = 4;                     //Set the group size to 4

        else if (customerRange <= groupSize6)       //Checks what sized group its going to be
            customerAmount = 6;                     //Set the group size to 6
    }
//--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();                                   //THE NavMesh Reference

        if (customerList.Count == 0)                                            //Checks to make sure there aren't any customers already
        {
            for (int i = 0; i < customerAmount; i++)                            //Checks how many customers its meant to have
            {
                customerGenerator = (Random.Range(1, customers.Count + 1));     //Randomly gerenates one of the customer prefabs
                Instantiate(customers[customerGenerator - 1], transform);       //Creates the prefab

            }
            foreach (Transform child in transform)                              //Searches the Object for children                          
            {
                if (child.tag == "Customer")                                    //Makes sure the children are customers
                {
                    customerList.Add(child.gameObject);                         //Populates customer list
                }
            }
        }
        Placement();                                                            //Runs the placement function 
    }
//--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    private void Update()
    {
        if (targetTable != null)                                                                    //Checks to see if the groups has been assigned a table
        {
            waiting = false;                                                                        //Set them to not waiting
        }

        if (gameObject != GameObject.Find("GameController")
            .GetComponent<GameController>().selectedGroup)                                          //Checks if the group is still currently selected by the game controller
            marker.SetActive(false);                                                                //Turns marker off

        if (waiting)                                                                                //Checks whether the table has a group on it
        {
            timer.gameObject.SetActive(true);                                                       //Sets up the multiplier for the timer (more customers = longer timer
            waitTime += Time.deltaTime;                                                             //Starts the timer
        }

        if (waitTime >= timer5)                                                                     //First timer node
            gameObject.transform.GetChild(0).transform.GetChild(4).gameObject.SetActive(false);     //Turns off

        if (waitTime >= timer4)                                                                     //Second timer
            gameObject.transform.GetChild(0).transform.GetChild(3).gameObject.SetActive(false);     //Turns off node

        if (waitTime >= timer3)                                                                     //Third timer
            gameObject.transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(false);     //Turns off node

        if (waitTime >= timer2)                                                                     //Fourth timer
            gameObject.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);     //Turns off node

        if (waitTime >= timer1)                                                                     //Last timer
        {
            timer.gameObject.SetActive(false);                                                      //Sets the whole timer off

            foreach (Transform child in gameObject.transform.GetChild(0))                           //Gets all the timer nodes
            {
                child.gameObject.SetActive(true);                                                   //Turns them back on         
            }
            targetTable = exit.GetComponent<BoxCollider>();                                         //Sets the groups target as the exit
            agent.SetDestination(exit.transform.position);                                          //Sets the groups destination as the exit
            gameObject.tag = "Leaving";                                                             //Changes the groups tag so the player cant interact with them
        }

        if (waiting == false)                                                                       //Checks to see if the group is waiting
        {
            timer.gameObject.SetActive(false);                                                      //Sets the whole timer off
        }
    }

//--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    private void Placement()
    {
        if (customerList.Count >= 1)                                                        //Putting the customer in the right location
        {
            customerList[0].transform.position = groupNodes[0].transform.position;
        }
        if (customerList.Count >= 2)                                                        //Putting the customer in the right location
        {
            customerList[1].transform.position = groupNodes[1].transform.position;
        }
        if (customerList.Count >= 3)                                                        //Putting the customer in the right location
        {
            customerList[2].transform.position = groupNodes[2].transform.position;
        }
        if (customerList.Count >= 4)                                                        //Putting the customer in the right location
        {
            customerList[3].transform.position = groupNodes[3].transform.position;
        }
        if (customerList.Count >= 5)                                                        //Putting the customer in the right location
        {
            customerList[4].transform.position = groupNodes[4].transform.position;
        }
        if (customerList.Count >= 6)                                                        //Putting the customer in the right location
        {
            customerList[5].transform.position = groupNodes[5].transform.position;
        }
    }

    public void MoveGroup(Vector3 newPosition)                                  // The move function for other controllers to reference
    {
        agent.SetDestination(newPosition);                                      // Tells the NavMeshAgent its looking for a new Vector3
    }


//--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
}

