using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class opentrigger : MonoBehaviour
{
    private void Start()
    {
        transform.GetComponent<DialogueTrigger>().TriggerDialogue();
    }
}
