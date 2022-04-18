using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Game
{
    public class CollisionChild : MonoBehaviour
    {
        [SerializeField] Stats parentWithStats;

        public Stats Parent => parentWithStats;
        private void Start()
        {
            gameObject.tag = parentWithStats.tag;
        }
        public void Hit(float damage)
        {
            parentWithStats.Hit(damage);
        }
        public void SetTargetHpBar(Image img)
        {
            parentWithStats.SetTargetHpBar(img);
        }
    }
}