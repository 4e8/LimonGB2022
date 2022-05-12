using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class AudioMixerController : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    string master = "Master";
    string fx = "FX";
    string car = "Car";
    string music = "Music";

    public void SetMaster(float sliderValue)
    {
        audioMixer.SetFloat(master, sliderValue);
    }
    public void SetFX(float sliderValue)
    {
        audioMixer.SetFloat(fx, sliderValue);
    }
    public void SetCar(float sliderValue)
    {
        audioMixer.SetFloat(car, sliderValue);
    }
    public void SetMusic(float sliderValue)
    {
        audioMixer.SetFloat(music, sliderValue);
    }
}
