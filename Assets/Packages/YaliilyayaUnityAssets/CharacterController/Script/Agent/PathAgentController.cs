using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(AgentController))]
public class PathAgentController : MonoBehaviour
{
    public float timeout;
    public List<GameObject> targets = new ();
    private AgentController agent;

    private int curentTargetIndex;
    private GameObject curentTarget;

    private float sleep = 0.5f; 
    void Start()
    {
        agent = GetComponent<AgentController>();
        curentTargetIndex = 0;
        curentTarget = null;
    }

    // После сортируем и выбираем первый ближайший
    
    // Есть вопрос, когда таргет в дали, то лист обновляемый не замети появление объекта в близи... Нужно ли искать каждый раз ближайший объект ?
    // Это нагрузит систему но сделает поведение более естественным. Например при сборе сундука
    void Update()
    {
        if (sleep > 0)
        {
            sleep -= Time.deltaTime;
            return;
        }

        if (curentTarget)
        {
            agent.MoveToPosition(curentTarget.transform.position);
        }

        if (curentTarget == null && targets != null && targets.Count > 0)
        {
            curentTarget = targets[curentTargetIndex % targets.Count];
        }
        else if (curentTarget 
            && agent.DistanceToTarget < 0.2f 
            && Vector3.Distance(agent.transform.position,curentTarget.transform.position) < 1
        ){   
            curentTarget = null;
            curentTargetIndex++;
            if (timeout > 0)
            {
                sleep = timeout;
            }
        }
    }
    
}
