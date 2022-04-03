using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game
{
    public class Stats : MonoBehaviour
    {
        [SerializeField] private float health = 10f;
        private float healthCurrent; 
        // Start is called before the first frame update
        void Start()
        {
            healthCurrent = health;
        }

        // Update is called once per frame
        void Update()
        {
            
        }
        public void Hit(float damage)
        {
            healthCurrent -= damage;
            if (healthCurrent <= 0) Destroy(gameObject);
        }
    }
}