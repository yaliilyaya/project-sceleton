using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class AgentController : MonoBehaviour
{
    public NavMeshAgent Agent { get; set; }
    private Vector3 targetPosition;

    public float StoppingDistance = 0.2f;
    
    private StarterAssetsThirdPersonAnimationController AnimationController; 
    
    public float DistanceToTarget { get; private set; }

    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        AnimationController = GetComponentInChildren<StarterAssetsThirdPersonAnimationController>();
        Agent.isStopped = true;
        targetPosition = transform.position;
    }

    void Update()
    {
        DistanceToTarget = Agent.hasPath ? CalculateDistance() : 0;

        Debug.DrawLine(transform.position, targetPosition);

        if (!Agent.isStopped && DistanceToTarget < 0.2f)
        {
            Agent.isStopped = true;
        }

        SetAnimationParams();
    }

    private float CalculateDistance()
    {
        var path = Agent.path;
        float lng = 0.0f;
       
        if (( path.status != NavMeshPathStatus.PathInvalid ) && ( path.corners.Length > 1 ))
        {
            for ( int i = 1; i < path.corners.Length; ++i )
            {
                lng += Vector3.Distance( path.corners[i-1], path.corners[i] );
            }
        }
       
        return lng;
    }

    private void SetAnimationParams()
    {
        var velocity = Agent.velocity.magnitude / Agent.speed;

        AnimationController?.SetSpeed(velocity * 4);
        AnimationController?.SetMotionSpeed(1.0f);
        AnimationController?.SetGrounded(false);
    }

    private void OnDrawGizmos()
    {
        if (!Agent)
        {
            return;
        }

        if (!Agent.isStopped)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(targetPosition + Vector3.up * 0.2f, 0.2f);
        }

        if (Agent.hasPath)
        {
            DrawGizmosDrawPath();
        }
    }

    public void MoveToPosition(Vector3 position)
    {
        Agent.SetDestination(targetPosition);
        Agent.stoppingDistance = StoppingDistance;
        
        Agent.isStopped = false;
        targetPosition = position;
    }
    
    void DrawGizmosDrawPath()
    {
        Gizmos.color = Color.red;
        
        var begin = transform.position;
        var end = targetPosition;
        
        if (Agent.path.corners.Length < 2)
        {
            Gizmos.DrawLine(begin, end);
            return;
        }

        foreach (var corner in Agent.path.corners)
        {
            Gizmos.DrawLine(begin, corner);
            begin = corner;
        }
        
        //Gizmos.DrawLine(begin, end);
    }
}
