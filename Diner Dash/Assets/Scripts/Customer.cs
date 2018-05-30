using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour{
//--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    NavMeshAgent agent;     //The NavMesh Reference

//--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();   //Getting NavMesh Reference
    }
//--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public void MoveGroup(Vector3 newPosition)      //The move function for other controllers to reference
    {        
        agent.SetDestination(newPosition);          //Tells the NavMeshAgent its looking for a new Vector3
    }
//--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
}
