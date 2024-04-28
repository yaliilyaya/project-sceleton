using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
[RequireComponent(typeof(LineRenderer))]
public class DrawPathAgent : MonoBehaviour
{
    private UnityEngine.AI.NavMeshAgent agent;
    private LineRenderer lineRenderer;
    
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.startWidth = 0.15f;
        lineRenderer.endWidth = 0.15f;
        lineRenderer.positionCount = 0;
    }

    void Update()
    {
        if (agent.hasPath)
        {
            DrawPath();
        }
    }

    void DrawPath()
    {
        lineRenderer.positionCount = agent.path.corners.Length;
        lineRenderer.SetPosition(0, transform.position);

        if (agent.path.corners.Length < 2)
        {
            return;
        }

        for (int i = 0; i < agent.path.corners.Length; i++)
        {
            var corner = agent.path.corners[i];
            lineRenderer.SetPosition(i, corner);
        }
    }
}
