using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    //could do it smooth and pretty but didnt find a way without using updates
    public class DoorButton : MonoBehaviour
    {
        [SerializeField] string requireTag;
        [SerializeField] GameObject door;
        [SerializeField] Animator animator;

        private bool open = false;
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == requireTag)
            {
                animator.SetBool("Open", !animator.GetBool("Open"));
                //animator.speed *= -1;
                //animator.Play("bridgeGates");

                //if (open == false) door.transform.Translate(0,-2,0);
                //else door.transform.Translate(0, 2, 0);
                //open = !open;
            }
        }
    }
}