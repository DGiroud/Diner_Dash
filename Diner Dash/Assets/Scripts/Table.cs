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



    public int cC;                                 // the amount of CHILDREN in the UNSEATED group
    public int sC;                                  // the amount of CUSTOMERS in the SEATED group

    private void Update()
    {
        if (seated)                                 //Checks whether the table has a group on it
        {
            float multiplier = 1 / (float)sC;
            seatedTimer += Time.deltaTime * multiplier;
            //Debug.Log(multiplier);
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
            cC = other.gameObject.transform.childCount;

            if (other.GetComponent<Group>().targetTable != GetComponent<BoxCollider>())
            {
                return;
            }          

            if (tableNodes.Count < other.GetComponent<Group>().customerList.Count)
            {
                return;

            }
            if (tableNodes.Count >= other.GetComponent<Group>().customerList.Count)
            {
                

                if (seated == false)
                {
                    for (int i = 0; i <= cC - 8; i++)
                    {
                        other.gameObject.transform.GetChild(7).SetParent(transform);
                        //Debug.Log("Child");
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
                //Debug.Log("Added");
            }
            
        }
        sC = seatedCustomers.Count;
        for (int i = 1; i <= sC; i++)
        {
            seatedCustomers[i - 1].transform.position = tableNodes[i - 1].transform.position;
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
            timer.gameObject.SetActive(false);
            for (int i = 1; i < transform.GetChildCount(); i++)
            {
                timer.transform.GetChild(i).gameObject.SetActive(true);
            }
            
            
            seated = false;
            Camera.main.GetComponent<GameController>().scoreAmount += score;
            foreach (Transform child in transform)
            {
                if (child.tag == "Customer")
                {
                    Destroy(child.gameObject);
                }
            }
            seatedCustomers = null;
            cC = 0;
            sC = 0;
            seatedTimer = 0;
            score = 1;


        }


       // sC = seatedCustomers.Count;
        //Debug.Log("seated");
        //Debug.Log(sC);


    }
}

   



