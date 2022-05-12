using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Bullet : MonoBehaviour
    {
        //[SerializeField] private float speed = 20f;
        [SerializeField] private float damage = 3f;
        [SerializeField] private GameObject boomPrefab;
        [SerializeField] private Transform spawnPoint;
        private Rigidbody rb;
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }
        private void FixedUpdate()
        {
            //transform.position += transform.forward * speed * Time.fixedDeltaTime;
            transform.forward = rb.velocity;
        }
        private void OnCollisionEnter(Collision collision)
        {
            //if (collision.gameObject.TryGetComponent(out Stats targetStats)) 
            //    targetStats.Hit(damage);

            if (collision.gameObject.TryGetComponent(out CollisionChild target))
                target.Hit(damage);

            Destroy(gameObject);
            if (boomPrefab != null) Explode();
        }
        private void Explode()
        {
            var boom = Instantiate(boomPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
}