using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TaskCarrySafeStepReadyMove : MonoBehaviour
{
    public TaskCarrySafe TaskCarrySafe { get; set; }
    public GameObject Safe;

    void Start()
    {
        Debug.Log("Начинаем доставку!!");
        TaskCarrySafe.MainPartnerAgent.Agent.speed = 1.25f;
        TaskCarrySafe.SelfPartnerAgent.Agent.speed = 1.27f;
        TaskCarrySafe.SelfPartnerAgent.StoppingDistance = 1.8f;

        Safe = TaskCarrySafe.Safe.GetComponent<ContainsMoveableObject>()?.CreateGObject();
        if (Safe)
        {
            TaskCarrySafe.Safe.gameObject.SetActive(false);
        }
        else
        {
            Safe = TaskCarrySafe.Safe;
        }
    }

    private void OnDisable()
    {
        Debug.Log("Завершаем доставку!!");
        TaskCarrySafe.MainPartnerAgent.Agent.speed = 3.5f;
        TaskCarrySafe.SelfPartnerAgent.Agent.speed = 3.5f;
        TaskCarrySafe.SelfPartnerAgent.StoppingDistance = 0.2f;
    }

    //TODO:: Нужно переписать на другую логику
    // Вместо 2х агентов нужно делать одного с 2мя персанажами и их анимациями
    // есть проблема, если вытягивать троекторию движний персонажей по длине, то она выходит из радиуса агента, что приводит к кализиям
    void Update()
    {
        TaskCarrySafe.MainPartnerAgent.MoveToPosition(TaskCarrySafe.Place.transform.position);
        TaskCarrySafe.SelfPartnerAgent.MoveToPosition(TaskCarrySafe.MainPartnerAgent.transform.position);
            
        // Переносим сейф
        var safeMovingPosition =  TaskCarrySafe.MainPartner.transform.position;
        Safe.transform.position = safeMovingPosition;
        Safe.transform.LookAt(TaskCarrySafe.SelfPartner.transform.position);

        if (isDoneMove())
        {
            enabled = false;
        }
    }

    private bool isDoneMove()
    {
        return TaskCarrySafe.MainPartnerAgent.DistanceToTarget < .6f
               && Vector3.Distance(TaskCarrySafe.MainPartnerAgent.transform.position, TaskCarrySafe.Place.transform.position) < 1.2f;
    }
    
    private void OnDrawGizmos()
    {
        if (TaskCarrySafe.MainPartnerAgent != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(TaskCarrySafe.MainPartnerAgent.transform.position + Vector3.up, 0.1f);
        }
    }
}
