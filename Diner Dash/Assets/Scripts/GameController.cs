using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{
    private int winCondition = 1000;

    public GameObject pointer;                                                          // The pointer for player feedback
    public GameObject selectedGroup;                                                    // The Current Group The Player Is Controlling
    public LayerMask raycastLayerMask;                                                  // The Raycast Layer Mask
    public Text score;
    public GameObject canvas;

    public GameObject onScreenUI;
    public GameObject winScreenUI;

    public int scoreAmount = 0;

    public GameObject emptyGroup;
    public GameObject exit;

    public GameObject marker;



    void Update()
    {
        score.text = scoreAmount.ToString();

        if (selectedGroup == null)
            selectedGroup = emptyGroup;

        if (scoreAmount >= winCondition)
        {
            WinScreen();
            canvas.transform.GetComponent<PauseMenu>().gameIsPaused = true;
        }

        RaycastHit hit;                                                                 //
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);                    // Where The Ray Comes From
        float distanceOfRay = 100;                                                      // How Far The Ray Shoots Out
        {
            if (Physics.Raycast(ray, out hit, distanceOfRay, raycastLayerMask))
            {

                if (Input.GetMouseButtonDown(0))
                {
                    if (canvas.transform.GetComponent<PauseMenu>().gameIsPaused == true)
                        return;                    

                    if (hit.transform.tag == "TableFull")
                        return;

                    if (selectedGroup != emptyGroup)
                    {
                        if (hit.transform.tag == "TableEmpty")                                      // Has to have the tag floor                    
                        {
                            selectedGroup.GetComponent<Group>().MoveGroup(hit.point);                   // Tells the group to follow the pointer
                            selectedGroup.GetComponent<Group>().targetTable = hit.transform.GetComponent<BoxCollider>();
                            selectedGroup = emptyGroup;
                            hit.transform.tag = "TableFull";
                        }

                        if (hit.transform.tag == "Exit")                                                       // Has to have the tag floor                    
                        {
                            selectedGroup.GetComponent<Group>().MoveGroup(hit.point);                          // Tells the group to follow the pointer
                            selectedGroup.GetComponent<Group>().targetTable = hit.transform.GetComponent<BoxCollider>();
                            selectedGroup = emptyGroup;
                        }
                    }

                    if (hit.transform.tag == "Group")
                    {
                        selectedGroup = hit.transform.gameObject;
                        selectedGroup.transform.GetChild(1).gameObject.SetActive(true);
                    }
                    if (hit.transform.tag == "Floor")                                       // Has to have the tag floor 
                    {
                        if (selectedGroup != emptyGroup)
                            selectedGroup.GetComponent<Group>().MoveGroup(hit.point);                   // Tells the group to follow the pointer
                    }

                    pointer.transform.position = hit.transform.position;                             // moves the pointer to the rays location
                }
            }
        }
    }

    private void WinScreen()
    {
        winScreenUI.SetActive(true);
        onScreenUI.SetActive(false);
        Time.timeScale = 0f;
    }
}
            
            
        
    




