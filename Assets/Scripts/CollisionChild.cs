using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game
{
    public class CollisionChild : MonoBehaviour
    {
        [SerializeField] Stats parentWithStats;
        private void Start()
        {
            gameObject.tag = parentWithStats.tag;
        }
        public void Hit(float damage)
        {
            parentWithStats.Hit(damage);
        }
    }
}