using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainsMoveableObject : MonoBehaviour
{
    public GameObject MoveablePrefab;

    public GameObject CreateGObject()
    {
        return !MoveablePrefab ? null : Instantiate(MoveablePrefab);
    }
}
