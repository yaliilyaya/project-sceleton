using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(AgentController))]
public class TargetStaticAgentController : MonoBehaviour
{
    public List<GameObject> targets = new ();
    private AgentController agent;

    private GameObject curentTarget;
    
    public GameObject objectTarget;

    void Start()
    {
        agent = GetComponent<AgentController>();
        curentTarget = null;
    }

    // После сортируем и выбираем первый ближайший
    
    // Есть вопрос, когда таргет в дали, то лист обновляемый не замети появление объекта в близи... Нужно ли искать каждый раз ближайший объект ?
    // Это нагрузит систему но сделает поведение более естественным. Например при сборе сундука
    void Update()
    {
        if (objectTarget)
        {
            agent.MoveToPosition(objectTarget.transform.position);
        }
        else if (!curentTarget && targets != null && targets.Count > 0)
        {
            var currentPosition = transform.position;
            
            //TODO:: вообще нужно анализировать длинну маршрута до цели
            var query = from target in targets
                where target != null
                orderby Vector3.Distance(target.transform.position, currentPosition)// descending
                select target;

            curentTarget = query.FirstOrDefault();
            if (!curentTarget)
            {
                //Костылина, ибо Юнити перетирает значение таргетов из редактора
                targets = null;
                return;
            }
            agent.MoveToPosition(curentTarget.transform.position);
        }
        else if (curentTarget && targets != null && agent.DistanceToTarget < 0.2f)
        {
            //Не хрена не удаляет, ибо Юнити перетирает значение таргетов из редактора
            targets.Remove(curentTarget);
            curentTarget = null;
        }
    }
    
}
