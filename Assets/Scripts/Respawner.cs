using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Respawner : MonoBehaviour
    {
        [SerializeField] public GameObject obj;
        //[SerializeField] Transform respawnPoint;
        //[SerializeField] int delay;
        private IEnumerator cor;

        public void Respawn()
        {
            cor = Respawn(3);
            StartCoroutine(cor);
        }
        public IEnumerator Respawn(float delay)
        {
            yield return new WaitForSeconds(delay);
            if (obj.gameObject.TryGetComponent(out Stats stats)) stats.Hit(-float.MaxValue);
            obj.transform.position = gameObject.transform.position;
            obj.SetActive(true);
        }
    }
}