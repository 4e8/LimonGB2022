using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{    
    public class FindTarget : MonoBehaviour
    {
        

        [SerializeField] private string targetTag;
        private Transform target;

        public Transform Target => target;
        public bool HasTarget => target != null;


        private void OnTriggerStay(Collider other)
        {
            if (other.tag.Equals(targetTag)) target = other.transform;
        }
        private void OnTriggerExit(Collider other)
        {
            if( other.tag.Equals(targetTag)) target = null;
        }
    }
}