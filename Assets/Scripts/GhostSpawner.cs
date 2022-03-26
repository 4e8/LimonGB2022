using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSpawner : MonoBehaviour
{
    public Object ghost;
    Transform[] spawnPoints;
    public float targetTime = Time.time+1f;
    // Update is called once per frame
    void Update()
    {
        targetTime -= Time.smoothDeltaTime;
        if (targetTime > 0f)
        {
            targetTime = Time.time + GameObject.FindGameObjectsWithTag("Ghost").Length;
            Spawn();
        }
    }

    void Spawn()
    {
        Object.Instantiate(ghost, spawnPoints[Random.Range(0, spawnPoints.Length)]);
    }
}
