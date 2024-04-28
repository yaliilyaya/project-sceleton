using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Ищет цель по тегу и задаёт как объект слежения для агента
[RequireComponent(typeof(TargetStaticAgentController))]
public class TagTargetObjectUpdater : MonoBehaviour
{

    public string targetTag; 
    // Start is called before the first frame update
    void Start()
    {
        var target = GameObject.FindGameObjectsWithTag(tag).FirstOrDefault();
        var targetStaticAgentController = GetComponent<TargetStaticAgentController>();

        targetStaticAgentController.objectTarget = target;
    }
    
}
