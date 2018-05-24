using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Group")
        {
            if (Camera.main.GetComponent<GameController>().selectedGroup == other.gameObject)
            {
                Camera.main.GetComponent<GameController>().selectedGroup = Camera.main.GetComponent<GameController>().emptyObject;
            }
            Destroy(other.gameObject);
            Debug.Log("GoodBye");
        }
    }


}

    

