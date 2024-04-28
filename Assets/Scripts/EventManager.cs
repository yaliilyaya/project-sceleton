using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent
{
    public string EventName;
}

public class TriggerColliderGameEvent : GameEvent
{
    public GameObject Target;
    public GameObject OnTriggerGameObject;
    
}

public class EventManager : MonoBehaviour
{
    public delegate void OnEventDelegate(GameEvent gameEvent);
    public OnEventDelegate onEvent;

    private static EventManager eventManagerInstance;

    public static EventManager GetInstance()
    {
        if (eventManagerInstance != null)
        {
            return eventManagerInstance;
        }

        eventManagerInstance = CreateEventManager();

        eventManagerInstance.onEvent += gameEvent => Debug.Log($"EventManager.onEvent - {gameEvent.EventName}"); 

        return eventManagerInstance;
    }

    private static EventManager CreateEventManager()
    {
        var eventManager = new GameObject("EventManager");
        
        return eventManager.AddComponent<EventManager>();
    }
}
