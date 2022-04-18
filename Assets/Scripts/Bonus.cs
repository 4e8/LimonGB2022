using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Bonus : MonoBehaviour
    {
        [SerializeField] string requireTag = "Player";
        [SerializeField] float heal;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == requireTag)
            {
                if (other.gameObject.TryGetComponent(out Stats targetStats))
                    targetStats.Hit(-heal);
                if (other.gameObject.TryGetComponent(out CollisionChild target))
                    target.Hit(-heal);

                    Destroy(gameObject);
            }
        }
    }
}