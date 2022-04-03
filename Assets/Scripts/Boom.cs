using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Boom : MonoBehaviour
    {
        [SerializeField] private float growSpeed = 10f;
        [SerializeField] private float damage = 20f;
        private float lifeTime = 0.5f;
        private Vector3 scaleChange;
        private void Awake()
        {
            scaleChange = new Vector3(growSpeed/10, growSpeed/10, growSpeed/10);
            lifeTime += Time.time;
        }
        private void FixedUpdate()
        {
            transform.localScale += scaleChange;
            if (lifeTime < Time.time) Destroy(gameObject);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Stats targetStats))
            {
                targetStats.Hit(damage);

            }
            
        }
    }
}