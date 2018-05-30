using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TutorialGameController : MonoBehaviour
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

    public float currentTimes;
    public Text currentSpeed;
    private bool isCrunchTime = false;    

    void Update()
    {

        if (canvas.GetComponent<TutorialPauseMenu>().gameIsPaused == false)
            {
            if (isCrunchTime == false)
            {
                Time.timeScale = 1.0f;
            }
            else if (isCrunchTime == true)
            {
                Time.timeScale = 1.5f;
            }
        }


        score.text = scoreAmount.ToString();
        currentTimes = Time.timeScale;

        

        if (selectedGroup == null)
            selectedGroup = emptyGroup;

        if (scoreAmount >= winCondition)
        {
            WinScreen();
            canvas.transform.GetComponent<TutorialPauseMenu>().gameIsPaused = true;
        }

        RaycastHit hit;                                                                 //
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);                    // Where The Ray Comes From
        float distanceOfRay = 100;                                                      // How Far The Ray Shoots Out
        {
            if (Physics.Raycast(ray, out hit, distanceOfRay, raycastLayerMask))
            {

                if (Input.GetMouseButtonDown(0))
                {
                    if (canvas.transform.GetComponent<TutorialPauseMenu>().gameIsPaused == true)
                        return;

                    if (hit.transform.tag == "TableFull")
                        return;

                    if (selectedGroup != emptyGroup)
                    {
                        if (hit.transform.tag == "TableEmpty")                                      // Has to have the tag floor                    
                        {
                            selectedGroup.GetComponent<TutorialGroup>().MoveGroup(hit.point);                   // Tells the group to follow the pointer
                            selectedGroup.GetComponent<TutorialGroup>().targetTable = hit.transform.GetComponent<BoxCollider>();
                            selectedGroup = emptyGroup;
                            hit.transform.tag = "TableFull";
                        }

                        if (hit.transform.tag == "Exit")                                                       // Has to have the tag floor                    
                        {
                            selectedGroup.GetComponent<TutorialGroup>().MoveGroup(hit.point);                          // Tells the group to follow the pointer
                            selectedGroup.GetComponent<TutorialGroup>().targetTable = hit.transform.GetComponent<BoxCollider>();
                            selectedGroup = emptyGroup;
                        }
                    }

                    if (hit.transform.tag == "Group")
                    {
                        selectedGroup = hit.transform.gameObject;

                        foreach (Transform child in selectedGroup.transform)
                            if (child.tag == "Customer")
                                child.transform.GetChild(0).gameObject.SetActive(true);
                    }

                    if (hit.transform.tag == "Floor")                                       // Has to have the tag floor 
                    {
                        if (selectedGroup != emptyGroup)
                            selectedGroup.GetComponent<TutorialGroup>().MoveGroup(hit.point);                   // Tells the group to follow the pointer
                    }

                    pointer.transform.position = hit.transform.position;                             // moves the pointer to the rays location
                }
            }
        }
    }

    public void WinScreen()
    {
        winScreenUI.SetActive(true);
        onScreenUI.SetActive(false);
        canvas.GetComponent<TutorialPauseMenu>().gameIsPaused = true;
        Time.timeScale = 0f;
    }

    public void NormalSpeed()
    {
        isCrunchTime = false;
        Debug.Log("steady as she goes");
    }

    public void CrunchTime()
    {
        isCrunchTime = true;
        Debug.Log ("IT'S CRUNCH TIME!!");
    }
}








