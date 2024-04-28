using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(AgentController))]
public class TargetMoseAgentController : MonoBehaviour
{
    private Camera mainCamera;
    private AgentController agent;
    
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        agent = GetComponent<AgentController>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hit))
            {
                agent.MoveToPosition(hit.point);
            }
        }
    }
}
