using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wasdController : MonoBehaviour
{
    private Rigidbody rb;
    public float keyMaxVelocityChange = 10f;
    public float keyWalkSpeed = 2f;

    private Vector3 forward;
    private Vector3 moveTo;
    
    private StarterAssetsThirdPersonAnimationController AnimationController; 
    
    private void Awake()
    {
        AnimationController = GetComponentInChildren<StarterAssetsThirdPersonAnimationController>();
        
        rb = GetComponent<Rigidbody>();
        forward = Vector3.forward;
        moveTo = Vector3.zero;
    }
    
    // Что нам нужно сделать
    // Первое чтобы объект двигался в том направлении которове нажал игрок wasd
    // Объект поворачивался в нужном направлении
    private void Update()
    {
        forward = Vector3.Normalize(forward + moveTo / 10);
        transform.forward = forward;
        
        
        var speedAnimation = Vector3.Distance(Vector3.zero, moveTo) * keyWalkSpeed;
        
        AnimationController?.SetSpeed(speedAnimation );
        AnimationController?.SetMotionSpeed(1.0f);
        
        AnimationController?.SetGrounded(false);
    }
   
    void FixedUpdate()
    {
        moveTo = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        
       // Calculate how fast we should be moving
        Vector3 targetVelocity = moveTo  * keyWalkSpeed;

        // Apply a force that attempts to reach our target velocity
        Vector3 velocity = rb.velocity;
        Vector3 velocityChange = (targetVelocity - velocity);
        velocityChange.x = Mathf.Clamp(velocityChange.x, -keyMaxVelocityChange, keyMaxVelocityChange);
        velocityChange.z = Mathf.Clamp(velocityChange.z, -keyMaxVelocityChange, keyMaxVelocityChange);
        velocityChange.y = 0.1f;

        rb.AddForce(velocityChange, ForceMode.VelocityChange);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, forward);
        
        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(transform.position, moveTo);
        
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, rb ? rb.angularVelocity : Vector3.zero);
      
    }
}
