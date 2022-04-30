using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class SwitchWeapon : MonoBehaviour
    {
        [SerializeField] GameObject weapon;
        [SerializeField] GameObject[] weapons;
            
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyUp(KeyCode.Z)) Switch(weapons[0]);
            if (Input.GetKeyUp(KeyCode.X)) Switch(weapons[1]);
            
        }
        void Switch(GameObject newWeapon)
        {
            if(weapon) weapon.active = false;
            weapon = Instantiate(newWeapon, transform);
            weapon.transform.position = transform.position;
            weapon.active = true;

        }
    }
}