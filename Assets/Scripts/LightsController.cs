using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class LightsController : MonoBehaviour
    {
        [SerializeField] Light[] frontLights;
        [SerializeField] Light[] backLights;
        [SerializeField] Light[] frontHalos;
        [SerializeField] Light[] backHalos;
        [SerializeField] Light[] stopHalos;
        bool lightsOn = false;

        public void SwitchLights()
        {
            foreach (Light light in frontLights) light.enabled = !light.enabled;
            foreach (Light light in frontHalos) light.enabled = !light.enabled;
            foreach (Light light in backLights) light.enabled = !light.enabled;
            foreach (Light light in backHalos) light.enabled = !light.enabled;
            lightsOn = !lightsOn;
        }
        public void SwitchLights(bool b)
        {
            foreach (Light light in frontLights) light.enabled = b;
            foreach (Light light in frontHalos) light.enabled = b;
            foreach (Light light in backLights) light.enabled = b;
            foreach (Light light in backHalos) light.enabled = b;
            lightsOn = b;
        }

        public void SwitchBreakLights()
        {
            if (lightsOn == false) foreach (Light light in backLights) light.enabled = !light.enabled;
            foreach (Light light in stopHalos) light.enabled = !light.enabled;
        }
        public void SwitchBreakLights(bool b)
        {
            if (lightsOn == false) foreach (Light light in backLights) light.enabled = b;
            foreach (Light light in stopHalos) light.enabled = b;
        }

    }
}