using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class TaskCarrySafe : MonoBehaviour, ITaskPartner
{
    public GameObject Player;
    
    public JobPartnerController MainPartner;
    public TargetStaticAgentController MainPartnerTargetAgent { get; set; }
    public AgentController MainPartnerAgent { get; set; }
    
    public JobPartnerController SelfPartner;
    public TargetStaticAgentController SelfPartnerTargetAgent { get; set; }
    public AgentController SelfPartnerAgent { get; set; }
    
    public GameObject Place;
    public GameObject Safe;
    
    private TaskCarrySafeStepStartMove TaskCarrySafeStepStartMove;
    private TaskCarrySafeStepReadyMove TaskCarrySafeStepReadyMove;

    void Start()
    {
        Debug.Log("Начинаем подготовку к операции");
        
        Place = Place ? Place : FindDeliveryPlace();

        MainPartnerTargetAgent = MainPartner.gameObject.GetComponent<TargetStaticAgentController>();
        SelfPartnerTargetAgent = SelfPartner.gameObject.GetComponent<TargetStaticAgentController>();
        
        MainPartnerTargetAgent.enabled = false;
        SelfPartnerTargetAgent.enabled = false;
        
        MainPartnerAgent = MainPartner.gameObject.GetComponent<AgentController>();
        SelfPartnerAgent = SelfPartner.gameObject.GetComponent<AgentController>();

        TaskCarrySafeStepStartMove = gameObject.AddComponent<TaskCarrySafeStepStartMove>();
        TaskCarrySafeStepStartMove.TaskCarrySafe = this;

        if (!MainPartnerAgent || !SelfPartnerAgent)
        {
            Destroy(this);
            Debug.Log("Нельзя начать операцию");
            
            return;
        }
        
        Debug.Log("Начинаем операцию!");
    }

    private GameObject FindDeliveryPlace()
    {
        var outPlaceList = from outPlace in GameObject.FindGameObjectsWithTag("OutPlace")
            orderby Vector3.Distance(outPlace.transform.position, transform.position)
            select outPlace;
        
        return outPlaceList.First();
    }

    void Update()
    {
        // Сначала нужно подойти к сейфу
        if (TaskCarrySafeStepStartMove && !TaskCarrySafeStepStartMove.enabled)
        {
            Debug.Log($"Партнёры на позици - Готов перетаскивать");
            TaskCarrySafeStepStartMove = null;

            TaskCarrySafeStepReadyMove = gameObject.AddComponent<TaskCarrySafeStepReadyMove>();
            TaskCarrySafeStepReadyMove.TaskCarrySafe = this;
        }
        // Нужно взять Сейф и проиграть анимацию
        // Перенести сейф на точку доставки
        if (TaskCarrySafeStepReadyMove && !TaskCarrySafeStepReadyMove.enabled)
        {
            TaskCarrySafeStepReadyMove = null;
            enabled = false;
        }
        // Нужно положить Сейф на точку достаки
    }

    private void OnDisable()
    {
        Debug.Log("Задача завершена, Сейф успешно сперт");

        MainPartnerTargetAgent.enabled = true;
        SelfPartnerTargetAgent.enabled = true;
            
        MainPartner.Task = null;
        MainPartner.isFree = true;
        SelfPartner.Task = null;
        SelfPartner.isFree = true;
        
        Destroy( gameObject.GetComponent<TaskCarrySafeStepStartMove>());
        Destroy( gameObject.GetComponent<TaskCarrySafeStepReadyMove>());
        Destroy(this);
    }

    // Прокоментируем Диалогом: 
    // ГГ: Вы 2е вынисите сейф
    // П1: Давая я спереди ты с зади подсоби
    // П2: хорошо, куда нести ?
    // П1: На точку у первого входа
    // П2: Понял, иди по медленние
    public void RunJob(
        GameObject player, 
        JobPartnerController mainPartner, 
        JobPartnerController selfPartner, 
        GameObject safe
    ) {
        Player = player;
        MainPartner = mainPartner;
        SelfPartner = selfPartner;
        Safe = safe;

        MainPartner.Task = this;
        MainPartner.isFree = false;
        SelfPartner.Task = this;
        SelfPartner.isFree = false;
    }


}
