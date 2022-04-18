using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game 
{
    [RequireComponent(typeof(FindTarget))]

    public class RangeAttack : MonoBehaviour
    {
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private Bullet bulletPrefab;
        [SerializeField] private float impulse;

        private FindTarget findTarget;
        

        private float AttackSpeed = 1;
        private float AttackTime;
        private float AttackDelay = 1;
        //public Component Stats;
        //public float AttackDamage = ;

        public float Impulse => impulse;
        public Transform Spawnpoint => spawnPoint;
        private void Start()
        {
            findTarget = GetComponent<FindTarget>();
        }
        private void Shoot()
        {
            var bullet = Instantiate(bulletPrefab,spawnPoint.position, spawnPoint.rotation);
            if (bullet.TryGetComponent(out Rigidbody body))
            {
                body.velocity = Vector3.zero;
                body.AddForce(impulse * spawnPoint.transform.forward, ForceMode.Impulse);
            }
        }

        // Update is called once per frame
        private void Update()
        {
            if (findTarget.HasTarget && (Time.time > AttackTime))
            {
                AttackTime = Time.time + AttackDelay;
                Shoot();
            }
        }
    }
}
