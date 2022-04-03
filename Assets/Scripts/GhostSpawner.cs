using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class GhostSpawner : MonoBehaviour
    {
        public Object ghost;
        public Transform[] spawnPoints;
        public float targetTime;
        // Update is called once per frame
        private void Start()
        {
            targetTime = Time.time + 1f;
        }
        void Update()
        {
            if (Time.time > targetTime)
            {
                Spawn();
            }
        }

        void Spawn()
        {
            Object.Instantiate(ghost);
            targetTime = Time.time + GameObject.FindGameObjectsWithTag("Ghost").Length * 10;
        }

    }
}