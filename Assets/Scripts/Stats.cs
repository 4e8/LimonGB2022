using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Game
{
    public class Stats : MonoBehaviour
    {
        [SerializeField] private Image hpbar;
        [SerializeField] private GuiHpBar hpbargui;
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
            if (hpbar) hpbar.fillAmount = healthCurrent/healthMax;
            if(hpbargui) hpbargui.SetValue(healthCurrent/healthMax);
        }
        private void Die()
        {
            gameObject.SetActive(false);
            resp.Respawn();
           
        }
        public void SetTargetHpBar(Image img)
        {
            hpbar = img;
            Hit(0);
        }
        public void SetTargetHpBarGUI(GuiHpBar img)
        {
            hpbargui = img;
            Hit(0);
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