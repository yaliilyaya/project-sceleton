using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(AgentController))]
public class TargetAgentController : MonoBehaviour
{
    public List<GameObject> targets { get; set; }
    private AgentController agent;

    public GameObject currentTarget;
    
    void Start()
    {
        agent = GetComponent<AgentController>();
        currentTarget = null;
    }

    // Ищем объекты по свойству СollectedItemTrigger и заполняем targets
    // После сортируем и выбираем первый ближайший
    
    // Есть вопрос, когда таргет в дали, то лист обновляемый не замети появление оюъекта в близи... Нужно ли искать каждый раз ближайший объект ?
    // Это нагрузит систему но сделает поведение более естественным. Например при сборе сундука
    
    // Есть баг когда агент уперся в ограничения навмеша и не может коснуться колайдера цели тогда он отпчиться на месте
    void Update()
    {
        // Debug.Log($"Дистануия до цели {agent.DistanceToTarget}");
        if (!currentTarget && targets != null && targets.Count > 0)
        {
            currentTarget = targets.FirstOrDefault();
            if (!currentTarget)
            {
                // Костылина, ибо Юнити перетирает значение таргетов из редактора
                targets = null;
                return;
            }
            agent.MoveToPosition(currentTarget.transform.position);
        }
        else if (currentTarget && targets != null && agent.DistanceToTarget < 0.2f)
        {
            // Не хрена не удаляет, ибо Юнити перетирает значение таргетов из редактора
            targets.Remove(currentTarget);
            currentTarget = null;
        }
        
    }
    
}
