using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float speed = 20f;
        [SerializeField] private float damage = 3f;
        [SerializeField] private GameObject boomPrefab;
        [SerializeField] private Transform spawnPoint;
        private void FixedUpdate()
        {
            transform.position += transform.forward * speed * Time.fixedDeltaTime;
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out Stats targetStats))
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