using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletShot : MonoBehaviour
{
    public Vector3 StartPosition;
    public float Speed = 0.05f;

    public float MaxDistanceLive = 0;
    public float MaxTimeLive = 0;
    private float TimeLive = 0;

    private void Awake()
    {
        StartPosition = transform.position;
    }

    void Update()
    {
        TimeLive += Time.deltaTime;
        if (MaxTimeLive > 0 && TimeLive > MaxTimeLive)
        {
            Destroy(gameObject);
        }

        if (MaxDistanceLive > 0 && Vector3.Distance(StartPosition, transform.position) > MaxDistanceLive)
        {
            Destroy(gameObject);
        }

        transform.position += transform.forward * Speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(StartPosition, transform.position);

        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, transform.forward);
    }
}
