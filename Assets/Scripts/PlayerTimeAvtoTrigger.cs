using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTimeAvtoTrigger : MonoBehaviour
{
    public string gameEventName = "PlayerTimeAutoTrigger";
    public float timeout = 5.5f;
    
    private float currentTime;
    private bool isEnableTimer;
    private bool isTriggerTimer;

    private GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;
        isEnableTimer = false;
        isTriggerTimer = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isEnableTimer)
        {
            currentTime += Time.deltaTime;
            // Debug.Log($"currentTime - {currentTime}");
        }

        if (!isTriggerTimer && currentTime > timeout)
        {
            isTriggerTimer = true;

            EventManager.GetInstance().onEvent(new TriggerColliderGameEvent
            {
                EventName = gameEventName,
                Target = gameObject,
                OnTriggerGameObject = player
            });

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            currentTime = 0;
            isEnableTimer = true;
            isTriggerTimer = false;

            player = other.gameObject;
        }

        // Debug.Log($"OnTriggerEnter isEnableTimer - {isEnableTimer}; isTriggerTimer - {isTriggerTimer}");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            currentTime = 0;
            isEnableTimer = false;
            isTriggerTimer = false;

            player = null;
        }
    }
}
