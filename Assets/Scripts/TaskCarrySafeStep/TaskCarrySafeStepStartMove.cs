using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskCarrySafeStepStartMove : MonoBehaviour
{
    public TaskCarrySafe TaskCarrySafe { get; set; }
    
    private Vector3 safePosition;
    
    void Start()
    {
        safePosition = TaskCarrySafe.Safe.transform.position;
    }

    void Update()
    {  
        TaskCarrySafe.MainPartnerAgent.MoveToPosition(safePosition);
        TaskCarrySafe.SelfPartnerAgent.MoveToPosition(safePosition);

        if (isPartnerOnPosition())
        {
            Debug.Log("Партнёры на позиции!");
            enabled = false;
        }
    }


    private bool isPartnerOnPosition()
    {
        return TaskCarrySafe.MainPartnerAgent.DistanceToTarget < .6f
               && TaskCarrySafe.SelfPartnerAgent.DistanceToTarget < .6f
               && Vector3.Distance(TaskCarrySafe.MainPartnerAgent.transform.position, safePosition) < 1.2f
               && Vector3.Distance(TaskCarrySafe.SelfPartnerAgent.transform.position, safePosition) < 1.2f;
    }
    
    private void OnDrawGizmos()
    {
        if (TaskCarrySafe.Safe != null) 
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(safePosition, 1f);
        }
    }
}
