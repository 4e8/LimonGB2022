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

        private FindTarget findTarget;

        private float AttackSpeed = 1;
        private float AttackTime;
        public float AttackDelay = 1;
        //public Component Stats;
        //public float AttackDamage = ;
        private void Start()
        {
            findTarget = GetComponent<FindTarget>();
        }
        private void Shoot()
        {
            var bullet = Instantiate(bulletPrefab,spawnPoint.position, spawnPoint.rotation);
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
