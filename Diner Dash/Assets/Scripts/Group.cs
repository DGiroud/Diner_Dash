using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Group : MonoBehaviour
{

    public GameObject customerPrefab;
    public List<GameObject> customerList = new List<GameObject>();          // List Of customers attached to the group
    public List<GameObject> groupNodes = new List<GameObject>();            // Location nodes for the customer placement
    public GameObject marker;

    public BoxCollider targetTable;

    public int customerAmount = 2;
    public int customerRange;

        
    public int groupSize2;
    public int groupSize4;
    public int groupSize6;


    //public Color Colour =Color.blue;


    NavMeshAgent agent;                                                         // The NavMesh Reference

    private void Awake()
    {
        customerRange = (Random.Range(1, 100));                                  //Makes a random number for reference

        if (customerRange != 0)
        {
            if (customerRange <= groupSize2)
            {
                customerAmount = 2;
            }
           else if (customerRange <= groupSize4)
            {
                customerAmount = 4;
            }
            else if (customerRange <= groupSize6)
            {
                customerAmount = 6;
            }
        }

    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();                                   // THE NavMesh Reference

        if (customerList.Count == 0)
        {
            for (int i = 0; i < customerAmount; i++)
            {
                Instantiate(customerPrefab, transform);
            }
            foreach (Transform child in transform)                                  //Searching the Object for children                          
            {
                if (child.tag == "Customer")                                        // Populating the Customer List
                {
                    customerList.Add(child.gameObject);
                }
            }
        }  
        
        Placement();
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "TableEmpty")
        {
            
        }
    }
    */
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

    

}

