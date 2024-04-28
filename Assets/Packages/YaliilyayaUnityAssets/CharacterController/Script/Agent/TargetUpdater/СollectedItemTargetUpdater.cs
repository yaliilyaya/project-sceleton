using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(TargetAgentController))]
public class СollectedItemTargetUpdater : MonoBehaviour
{
    private Scene scene;

    public float timeUpdate = 1;
    private float currentTimeUpdate = 0;

    private TargetAgentController targetAgent;

    private void Awake()
    {
        targetAgent = GetComponent<TargetAgentController>();
        scene = SceneManager.GetActiveScene();
    }

    // Ищем объекты по свойству СollectedItemTrigger и заполняем targets
    // После сортируем и выбираем первый ближайший
    void Update()
    {
        currentTimeUpdate -= Time.deltaTime;
        if (currentTimeUpdate > 0)
        {
            return;
        }
        currentTimeUpdate = timeUpdate;
        
        var targets = FindObjectsInScene<СollectedItemTrigger>();
        targets = SortTargets(targets);
        
        targetAgent.targets = targets;
    }

    // Сортируем объекты по приоритету
    // По дальности от объекта
    // По сортировке СollectedItemTrigger.Sort
    private List<GameObject> SortTargets(List<GameObject> targets)
    {
        var currentPosition = transform.position;

        var maxSortTarget = targets.Count > 0
            ? (
                from target in targets
                select target.GetComponent<СollectedItemTrigger>().sort
            ).Max()
            : 0;
        
        // Debug.Log($"maxSortTarget {maxSortTarget}");
        
        //TODO:: Вообще нужно анализировать длинну маршрута до цели
        var query = from target in targets
            let collectedItem = target.GetComponent<СollectedItemTrigger>()
            where target != null && Mathf.Approximately(collectedItem.sort, maxSortTarget)
            orderby Vector3.Distance(target.transform.position, currentPosition)// descending
            select target;
        
        return query.ToList();
    }

    // Ищем все Объекты на сцене с указанным компонентам
    private List<GameObject> FindObjectsInScene<T>()
    {
        var targets = new List<GameObject>();
        
        foreach (var gObject in scene.GetRootGameObjects())
        {
            foreach (var collectedItem in gObject.GetComponentsInChildren<СollectedItemTrigger>())
            {
                if (collectedItem.enabled)
                {
                    targets.Add(collectedItem.gameObject); 
                }
            }
        }

        return targets;
    }
}
