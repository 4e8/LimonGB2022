using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class Fuel : MonoBehaviour
    {
        [SerializeField] Image bar;
        [SerializeField] float fuelCapacity = 300;
        float fuelCurrent;
        float engineConsumptionRate = 1;

        bool empty = false;
        public bool Empty => empty;
        float test;

        // Start is called before the first frame update
        void Start()
        {
            fuelCurrent = fuelCapacity;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            test = Input.GetAxis("Vertical") * engineConsumptionRate;
            if (fuelCurrent > 0)
                fuelCurrent -= (0.01f * engineConsumptionRate) + (Input.GetAxis("Vertical") * engineConsumptionRate * 0.01f);
            else empty = true;
            bar.fillAmount = fuelCurrent / fuelCapacity;
        }
        public void Fill(float f)
        {
            fuelCurrent += f;
            if (fuelCurrent < fuelCapacity) fuelCurrent = fuelCapacity;
        }
    }
}