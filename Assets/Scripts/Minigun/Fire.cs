using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{

    public class Fire : MonoBehaviour
    {
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private Bullet bulletPrefab;
        [SerializeField] private Transform target;


        private float AttackSpeed = 1;
        private float AttackTime;
        public float AttackDelay = 1;
        //public Component Stats; 
        //public float AttackDamage = ;

        private void Shoot()
        {
            var bullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
        }

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetKey(KeyCode.Mouse0) && (Time.time > AttackTime))
            {
                AttackTime = Time.time + AttackDelay;
                Shoot();
            }
        }
    }
}
