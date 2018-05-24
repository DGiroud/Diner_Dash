using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Table : MonoBehaviour
{

    public List<GameObject> tableNodes;             //Location of the seats
    public List<GameObject> seatedCustomers;        //Customers once they're seated
    public GameObject timer;                        //Timer visual feedback
    public float seatedTimer;                       //How long the customers have been seated
    public int score = 1;                           //Base Score Multiplier
    public bool seated = false;                     //If the table already has a group on it

    private int timer5 = 1;                         //Timer5
    private int timer4 = 2;                         //Timer4
    private int timer3 = 3;                         //Timer3
    private int timer2 = 4;                         //Timer2
    private int timer1 = 5;                         //Timer1



    private int cC;                                 // the amount of CHILDREN in the UNSEATED group
    public int sC;                                  // the amount of CUSTOMERS in the SEATED group

    private void Update()
    {

        if (seated)                                 //Checks whether the table has a group on it
        {
            float multiplier = 1 / (float)sC;
            seatedTimer += Time.deltaTime * multiplier;
            Debug.Log(multiplier);
        }
        if (seatedTimer >= timer5)                  //First timer
        {
            Seated();                               //Refrences the 
        }
        if (seatedTimer >= timer4)
        {
            Seated();
        }
        if (seatedTimer >= timer3)
        {
            Seated();
        }
        if (seatedTimer >= timer2)
        {
            Seated();
        }
        if (seatedTimer >= timer1)
        {
            Seated();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
       

        if (other.tag == "Group")
        {
            if (other.GetComponent<Group>().targetTable != GetComponent<BoxCollider>())
            {
                return;
            }
            cC = other.gameObject.transform.childCount;

            if (tableNodes.Count < other.GetComponent<Group>().customerList.Count)
            {
                return;

            }
            if (tableNodes.Count >= other.GetComponent<Group>().customerList.Count)
            {

                if (seated == false)
                {

                    if (cC >= 13)
                    {
                        other.gameObject.transform.GetChild(12).SetParent(transform);                        
                    }
                    if (cC >= 12)
                    {
                        other.gameObject.transform.GetChild(11).SetParent(transform);                       
                    }
                    if (cC >= 11)
                    {
                        other.gameObject.transform.GetChild(10).SetParent(transform);                        
                    }
                    if (cC >= 10)
                    {
                        other.gameObject.transform.GetChild(9).SetParent(transform);                        
                    }
                    if (cC >= 9)
                    {
                        other.gameObject.transform.GetChild(8).SetParent(transform);
                        other.gameObject.transform.GetChild(7).SetParent(transform);
                        
                    }
                    seated = true;
                    Seating();
                    if (Camera.main.GetComponent<GameController>().selectedGroup == other.gameObject)
                    {
                        Camera.main.GetComponent<GameController>().selectedGroup = Camera.main.GetComponent<GameController>().emptyObject;
                    }
                    Destroy(other.gameObject);                    
                }
            }


        }

    }

    private void Seating()
    {
        foreach (Transform child in transform)                                  //Searching the Object for children                          
        {
            if (child.tag == "Customer")                                        // Populating the Customer List
            {
                seatedCustomers.Add(child.gameObject);

            }
        }
        if (seatedCustomers.Count >= 1)                                                        //Putting the customer in the right location
        {
            seatedCustomers[0].transform.position = tableNodes[0].transform.position;
            sC = 1;
        }
        if (seatedCustomers.Count >= 2)                                                        //Putting the customer in the right location
        {
            seatedCustomers[1].transform.position = tableNodes[1].transform.position;
            sC = 2;
        }
        if (seatedCustomers.Count >= 3)                                                        //Putting the customer in the right location
        {
            seatedCustomers[2].transform.position = tableNodes[2].transform.position;
            sC = 3;
        }
        if (seatedCustomers.Count >= 4)                                                        //Putting the customer in the right location
        {
            seatedCustomers[3].transform.position = tableNodes[3].transform.position;
            sC = 4;
        }
        if (seatedCustomers.Count >= 5)                                                        //Putting the customer in the right location
        {
            seatedCustomers[4].transform.position = tableNodes[4].transform.position;
            sC = 5;
        }
        if (seatedCustomers.Count >= 6)                                                        //Putting the customer in the right location
        {
            seatedCustomers[5].transform.position = tableNodes[5].transform.position;
            sC = 6;
        }
        Seated();
    }

    private void Seated()
    {
        score = (sC * 10);

        if (seated == false)
        {
            return;
        }
        timer.gameObject.SetActive(true);

        if (seatedTimer >= timer5)
        {
            gameObject.transform.GetChild(0).transform.GetChild(4).gameObject.SetActive(false);            
        }
        if (seatedTimer >= timer4)
        {
            gameObject.transform.GetChild(0).transform.GetChild(3).gameObject.SetActive(false);
        }
        if (seatedTimer >= timer3)
        {
            gameObject.transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(false);
        }
        if (seatedTimer >= timer2)
        {
            gameObject.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);
        }
        if (seatedTimer >= timer1)
        {
            gameObject.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            gameObject.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
            gameObject.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true);
            gameObject.transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(true);
            gameObject.transform.GetChild(0).transform.GetChild(3).gameObject.SetActive(true);
            gameObject.transform.GetChild(0).transform.GetChild(4).gameObject.SetActive(true);
            seated = false;
            Camera.main.GetComponent<GameController>().scoreAmount += score;
            foreach (Transform child in transform)
            {
                if (child.tag == "Customer")
                {
                    Destroy(child.gameObject);
                }
            }



        }


        sC = seatedCustomers.Count;
        //Debug.Log("seated");
        //Debug.Log(sC);


    }
}

   



