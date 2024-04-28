using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wasdAndMouseController : MonoBehaviour
{
    private Rigidbody rb;
    public float keyMaxVelocityChange = 10f;
    public float keyWalkSpeed = 5f;
    //TODO:: настроить чувствительность
    public float mouseMaxVelocityChange = 10f;
    public float mouseWalkSpeed = 5f;
    
    private Vector3 forward;
    private Vector3 moveTo;
   
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        forward = Vector3.forward;
        moveTo = Vector3.zero;
    }
    
    // Что нам нужно сделать
    // Первое чтобы объект двигался в том направлении которове нажал игрок wasd
    // Объект поворачивался в направлении относительно персонажа 
    // нужно ли двигаться к мыши ? 
    private void Update()
    {
        transform.forward = forward;
    }
   
    void FixedUpdate()
    {
        
        var directionPosition = FindDirectionMousePosition(); 
        forward = Vector3.Normalize(directionPosition);
        
        moveTo = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        
        // Calculate how fast we should be moving
        var targetVelocity = moveTo  * keyWalkSpeed;

        // Apply a force that attempts to reach our target velocity
        var velocity = rb.velocity;
        var velocityChange = (targetVelocity - velocity);
        velocityChange.x = Mathf.Clamp(velocityChange.x, -keyMaxVelocityChange, keyMaxVelocityChange);
        velocityChange.z = Mathf.Clamp(velocityChange.z, -keyMaxVelocityChange, keyMaxVelocityChange);
        velocityChange.y = 0;

        rb.AddForce(velocityChange, ForceMode.VelocityChange);
    }

    // private void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawRay(transform.position, forward);    
    // }

    private Vector3 FindDirectionMousePosition()
    {
        var objScreenPosition = Camera.main!.WorldToScreenPoint(transform.position);
        var directionMouseScreenPosition = objScreenPosition - Input.mousePosition;

        var  directionPosition = new Vector3(-directionMouseScreenPosition.x, 0, -directionMouseScreenPosition.y);
        
        return directionPosition; 
    }
}
