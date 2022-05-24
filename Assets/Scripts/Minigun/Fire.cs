using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{    
    public class Fire : MonoBehaviour
    {
        [SerializeField] WeaponGunConfig config;

        [SerializeField] Transform spawnPoint;
        [SerializeField] Bullet bulletPrefab;
        [SerializeField] float impulse;
        [SerializeField] float AttackDelay = 1;

        [SerializeField] Animator animator;
        public float Impulse => impulse;

        float AttackTime;
        PlayerTurretDirection turretDirection;
        private void Start()
        {
            if (TryGetComponent<PlayerTurretDirection>(out PlayerTurretDirection turretDirection)) this.turretDirection = turretDirection;
            bulletPrefab = config.bulletPrefab;
            impulse = config.impulse;
            AttackDelay = config.shootDelay;
        }
        private void Shoot()
        {
            var bullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
            if (bullet.TryGetComponent(out Rigidbody body))
            {
                body.velocity = Vector3.zero;
                body.AddForce(impulse * spawnPoint.transform.forward, ForceMode.Impulse);
            }
            //animator.SetBool("Firing", true);
            if (animator) animator.Play("minigunFire");
        }

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetKey(KeyCode.Mouse0) && turretDirection.ReadyToFire && (Time.time > AttackTime))
            {
                AttackTime = Time.time + AttackDelay;
                Shoot();
            }
        }
    }
}
