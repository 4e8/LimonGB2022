using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game
{
    public class Stats : MonoBehaviour
    {
        [SerializeField] private float healthMax = 10f;
        private float healthCurrent;

        [SerializeField] private GameObject spawnPoint;
        private Respawner resp;

        void Start()
        {
            healthCurrent = healthMax;
            if (spawnPoint.gameObject.TryGetComponent(out Respawner resp)) this.resp = resp;
            resp.obj = gameObject;
        }
        public void Hit(float damage)
        {
            healthCurrent -= damage;
            if (healthCurrent <= 0) Die();
            if (healthCurrent > healthMax) healthCurrent = healthMax;
        }
        private void Die()
        {
            resp.Respawn();
            gameObject.SetActive(false);
           
        }
        //private IEnumerator Respawn(float delay)
        //{
        //    yield return new WaitForSeconds(delay);
        //    healthCurrent = healthMax;
        //    gameObject.transform.position = spawnPoint.position;
        //    gameObject.SetActive(true);
        //}
    }
}