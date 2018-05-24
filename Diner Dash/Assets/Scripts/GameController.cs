using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{

    public GameObject pointer;                                                          // The pointer for player feedback
    public GameObject selectedGroup;                                                            // The Current Group The Player Is Controlling
    public LayerMask raycastLayerMask;                                                  // The Raycast Layer Mask
    public Text score;

    public int scoreAmount = 0;
    
    public GameObject emptyObject;

    void Update()
    {
        score.text = scoreAmount.ToString();

        RaycastHit hit;                                                                 //
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);                    // Where The Ray Comes From
        float distanceOfRay = 100;                                                      // How Far The Ray Shoots Out
        {
            if (Physics.Raycast(ray, out hit, distanceOfRay, raycastLayerMask))

            {
                if (Input.GetMouseButtonDown(1))
                {
                    if (selectedGroup != emptyObject)
                    {
                        selectedGroup.gameObject.transform.GetChild(0).gameObject.SetActive(false);
                    }
                    selectedGroup = emptyObject;                
                }

                if (Input.GetMouseButtonDown(0))
                {

                    if (hit.transform.tag == "TableEmpty")                                       // Has to have the tag floor 
                    {
                        if (selectedGroup != emptyObject)
                        {
                            selectedGroup.gameObject.transform.GetChild(0).gameObject.SetActive(false);
                            selectedGroup.GetComponent<Group>().MoveGroup(hit.point);                   // Tells the group to follow the pointer
                            selectedGroup.GetComponent<Group>().targetTable = hit.transform.GetComponent<BoxCollider>();
                        }
                        pointer.transform.position = hit.point;                             // moves the pointer to the rays location
                        
                    }
                    if (hit.transform.tag == "Group")
                    {
                        if (selectedGroup != emptyObject)
                        {
                            selectedGroup.gameObject.transform.GetChild(0).gameObject.SetActive(false);
                        }
                        pointer.transform.position = hit.point;                             // moves the pointer to the rays location
                        selectedGroup = hit.transform.gameObject;
                        selectedGroup.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    }
                    if (hit.transform.tag == "Floor")                                       // Has to have the tag floor 
                    {
                        pointer.transform.position = hit.point;                             // moves the pointer to the rays location
                        if (selectedGroup != emptyObject)
                        {
                            selectedGroup.GetComponent<Group>().MoveGroup(hit.point);                   // Tells the group to follow the pointer
                        }
                    }
                }
            }
        }
    }
}
            
            
        
    


/*
void OnMouseDrag ()
{
Debug.Log("Dragging Mouse");
RaycastHit hit;                                                                 //
Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);                    // Where The Ray Comes From
float distanceOfRay = 100;                                                      // How Far The Ray Shoots Out
if (Physics.Raycast(ray, out hit, distanceOfRay, raycastLayerMask))
{
    if (Input.GetMouseButtonUp(0))                                                              // Releasing the mouse button
    {
        if (hit.transform.tag == "Floor")                                       // Has to have the tag floor 
        {
            pointer.transform.position = hit.point;                             // moves the pointer to the rays location
            group.GetComponent<Group>().MoveGroup(hit.point);                   // Tells the group to follow the pointer
        }
    }
}

}
*/

