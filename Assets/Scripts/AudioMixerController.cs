using UnityEngine;
using UnityEngine.Audio;


public class AudioMixerController : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    string _master = "Master";
    string _fx = "FX";
    string _car = "Car";
    string _music = "Music";

    public void SetMaster(float sliderValue)
    {
        audioMixer.SetFloat(_master, sliderValue);
    }
    public void SetFX(float sliderValue)
    {
        audioMixer.SetFloat(_fx, sliderValue);
    }
    public void SetCar(float sliderValue)
    {
        audioMixer.SetFloat(_car, sliderValue);
    }
    public void SetMusic(float sliderValue)
    {
        audioMixer.SetFloat(_music, sliderValue);
    }
}
