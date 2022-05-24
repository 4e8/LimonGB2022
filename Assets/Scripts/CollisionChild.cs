using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Game
{
    public class CollisionChild : MonoBehaviour
    {
        [SerializeField] Stats parentWithStats;
        [SerializeField] float armorMax = 0;
        float armorCurrent;

        public Stats Parent => parentWithStats;
        private void Start()
        {
            gameObject.tag = parentWithStats.tag;
            armorCurrent = armorMax;
        }
        public void Hit(float damage)
        {
            if (damage > armorCurrent)
            {
                armorCurrent -= damage / 10;
                if (armorCurrent < 0) armorCurrent = 0;
                parentWithStats.Hit(damage - armorCurrent);
            }
        }
        public void Repair(float damage)
        {
            armorCurrent += damage;
            if (armorCurrent > armorMax) armorCurrent = armorMax;
        }
        public void SetTargetHpBar(Image img)
        {
            parentWithStats.SetTargetHpBar(img);
        }
        public void SetTargetHpBar(GuiHpBar img)
        {
            parentWithStats.SetTargetHpBarGUI(img);
        }
    }
}