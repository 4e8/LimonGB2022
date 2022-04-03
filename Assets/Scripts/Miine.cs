using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Miine : MonoBehaviour
    {
        [SerializeField] private float damage = 10f;
        [SerializeField] private GameObject boomPrefab;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private string targetTag;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag != targetTag) return;
            if (other.gameObject.TryGetComponent(out Stats targetStats))
            {
                targetStats.Hit(damage);

            }

            Destroy(gameObject);
            Explode();
        }
        private void Explode()
        {
            var boom = Instantiate(boomPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
}