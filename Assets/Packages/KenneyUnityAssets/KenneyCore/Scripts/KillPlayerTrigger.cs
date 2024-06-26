using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayerTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(other.gameObject);
        }
    }
}
