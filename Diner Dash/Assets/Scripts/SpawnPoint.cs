using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{

    public GameObject group;
    public Transform currentTransform;

    public GameObject spawnedGroup;

    public float timer;
    public float countdownTimer;
    public bool full = false;

    private void Awake()
    {
        CountdownTimerF();
    }

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
                CountdownTimerF();
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

    void CountdownTimerF()
    {
        timer = Random.Range(5, 15);
    }
}
