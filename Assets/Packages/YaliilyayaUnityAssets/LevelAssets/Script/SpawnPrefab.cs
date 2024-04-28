using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPrefab : MonoBehaviour
{
    public GameObject gameItemPrefab;
    public float timeout = 0;
    public float period = 0;

    private float startTime = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        if (timeout <= 0)
        {
            Spawn();
            if (period > 0)
            {
                timeout = period;
            }
        }
    }

    private void Update()
    {
        startTime += Time.deltaTime;

        if (timeout > 0 && startTime >= timeout)
        {
            Spawn();
            if (period > 0)
            {
                timeout = period;
                startTime = 0;
            }
        }
    }

    private void Spawn()
    {
        var gameItem = Instantiate(gameItemPrefab);
        gameItem.transform.position = transform.position;

        if (period <= 0)
        {
            Destroy(this.gameObject);
        }
    }

}
