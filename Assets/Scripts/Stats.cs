using UnityEngine;
using UnityEngine.UI;


namespace Game
{
    public class Stats : MonoBehaviour
    {
        [SerializeField] Image _hpbar;
        [SerializeField] GuiHpBar _hpbargui;
        [SerializeField] float _healthMax = 10f;
        float _healthCurrent;

        [SerializeField] GameObject _spawnPoint;
        Respawner _resp;

        private void Start()
        {
            _healthCurrent = _healthMax;
            if (_spawnPoint.gameObject.TryGetComponent(out Respawner resp)) _resp = resp;
            resp.obj = gameObject;
        }
        public void Hit(float damage)
        {
            _healthCurrent -= damage;
            if (_healthCurrent <= 0) Die();
            if (_healthCurrent > _healthMax) _healthCurrent = _healthMax;
            if (_hpbar) _hpbar.fillAmount = _healthCurrent/_healthMax;
            if(_hpbargui) _hpbargui.SetValue(_healthCurrent/_healthMax);
        }
        private void Die()
        {
            gameObject.SetActive(false);
            _resp.Respawn();
           
        }
        public void SetTargetHpBar(Image img)
        {
            _hpbar = img;
            Hit(0);
        }
        public void SetTargetHpBarGUI(GuiHpBar img)
        {
            _hpbargui = img;
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