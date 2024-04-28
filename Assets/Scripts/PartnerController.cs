using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PartnerController : MonoBehaviour
{
    public List<GameObject> partners;
    private PartnerPlaceController PartnerPlaceController;
    
    private void Awake()
    {
        EventManager.GetInstance().onEvent += (gameEvent) =>
        {
            if (gameEvent.EventName == "StealSafe")
            {
                var triggerColliderGameEvent = (TriggerColliderGameEvent)gameEvent;
                OnPlayerTimeAutoTrigger(triggerColliderGameEvent.Target, triggerColliderGameEvent.OnTriggerGameObject);
            }
        };

        PartnerPlaceController = GetComponent<PartnerPlaceController>();
    }

    void Start()
    {
        PartnerPlaceController.placeOnGlasses(partners);
    }

    void OnPlayerTimeAutoTrigger(GameObject target, GameObject triggerGameObject)
    {
        Debug.Log($"OnPlayerTimeAutoTrigger - {target.name} - {triggerGameObject.name}");
        
        // Для начала определить кто свободен
        var freePartners = FindFreePartners();
        Debug.Log($"freePartners.Count - {freePartners.Count}");
        // Найти 2х человек
        if (freePartners.Count < 2)
        {
            EventManager.GetInstance().onEvent(new GameEvent { EventName = "NotFound-freePartners" });
            return;
        }

        var mainPartner = freePartners[0];
        var selfPartner = freePartners[1];
        // Назначить работу
        // Назначить 2х человек
        // назначить сейф
        mainPartner.gameObject.AddComponent<TaskCarrySafe>().RunJob(
            gameObject,
            mainPartner,
            selfPartner,
            target.transform.parent.gameObject
        );

        Debug.Log($"TaskCarrySafe - {mainPartner.name} - {selfPartner.name}");
    }

    public List<JobPartnerController> FindFreePartners()
    {
        var freePartners = from partner in partners
            let jobPartnerController = partner.GetComponent<JobPartnerController>()
            where jobPartnerController != null && jobPartnerController.isFree
            orderby Vector3.Distance(transform.position, partner.transform.position)
            select jobPartnerController;
        
        return freePartners.ToList(); 
    }
}
