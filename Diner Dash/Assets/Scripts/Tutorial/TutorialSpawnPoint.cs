using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSpawnPoint : MonoBehaviour
{

    public GameObject group;
    public Transform currentTransform;

    public GameObject spawnedGroup;

    public float timer;
    public float countdownTimer;
    public bool full = false;
    public GameObject dialogue;
    

    private void Update()
    {
        if (full == false)
        {
            countdownTimer += Time.deltaTime;
            if (countdownTimer >= timer)
            {
                Instantiate(group, currentTransform.position, currentTransform.rotation);
                full = true;
                countdownTimer = 0.0f;
                
                if (dialogue == null)                
                    return;

                dialogue.GetComponent<DialogueTrigger>().TriggerDialogue();
            }
        }


    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Group")
        {
            full = false;
        }
        if (other.tag == "Leaving")
        {
            full = false;
        }
    }

    
}
