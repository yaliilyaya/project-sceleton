using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class СollectedItemTrigger : MonoBehaviour
{
    public float sort = 100;
    public GameObject destroyPrefab;

    private void Awake()
    {
        if (!GetComponent<Collider>())
        {
            // Если нет колайдера компонент не будет работать
            Destroy(this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (destroyPrefab)
        {
            Instantiate(destroyPrefab, transform.position, transform.rotation);
        }

        Destroy(gameObject);
    }
}
