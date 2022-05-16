using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game 
{
    [RequireComponent(typeof(FindTarget))]

    public class RangeAttack : MonoBehaviour
    {
        [SerializeField] Transform spawnPoint;
        [SerializeField] Bullet bulletPrefab;
        [SerializeField] float impulse;
        [SerializeField] float AttackDelay = 1;

        FindTarget findTarget;

        TurretDirection turretDirection;
        float AttackTime;


        public float Impulse => impulse;
        public Transform Spawnpoint => spawnPoint;
        private void Start()
        {
            if (TryGetComponent<TurretDirection>(out TurretDirection turretDirection)) this.turretDirection = turretDirection;
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

        private void Update()
        {
            if (findTarget.HasTarget && turretDirection.ReadyToFire && (Time.time > AttackTime))
            {
                AttackTime = Time.time + AttackDelay;
                Shoot();
            }
        }
    }
}
