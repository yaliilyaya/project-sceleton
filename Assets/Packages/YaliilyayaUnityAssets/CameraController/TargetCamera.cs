using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCamera : MonoBehaviour
{
    public bool lockPositionX = false;
    public bool lockPositionY = false;
    public bool lockPositionZ = false;
    
    public bool lockRotationX = false;
    public bool lockRotationY = false;
    public bool lockRotationZ = false;

    public GameObject target;

    private Vector3 position;
    private Vector3 rotation;

    private void Awake()
    {
        position = target.transform.position;
        rotation = target.transform.eulerAngles;
    }

    private void Update()
    {
        var diffPosition = new Vector3(
            target.transform.position.x - position.x,
            target.transform.position.y - position.y,
            target.transform.position.z - position.z 
        );
        var currentTargetRotation = target.transform.eulerAngles;
        var diffRotation = new Vector3(
            currentTargetRotation.x - rotation.x,
            currentTargetRotation.y - rotation.y,
            currentTargetRotation.z - rotation.z 
        );

        transform.position = new Vector3(
            transform.position.x + diffPosition.x,
            transform.position.y + diffPosition.y,
            transform.position.z + diffPosition.z
        );

        var eulerAngles = transform.eulerAngles;
        /*
        transform.eulerAngles = new Vector3(
            eulerAngles.x + diffRotation.x,
            eulerAngles.y + diffRotation.y,
            eulerAngles.z + diffRotation.z
        );
        */
        
        position = target.transform.position;
        rotation = target.transform.eulerAngles;
    }

}
