using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(CC))]
    public class CarSoundController : MonoBehaviour
    {
        [SerializeField] AudioSource carSource;
        [SerializeField] AudioClip engineClip;
        [SerializeField] AudioClip skidClip;

        [SerializeField] AudioSource[] wheelAudioSources;
        [SerializeField] WheelCollider[] test;
        
        
        
        CC cc;
        private void Start()
        {
            cc = GetComponent<CC>();
            wheelAudioSources = new AudioSource[cc.Wheels.Length];
            for (int i = 0; i < cc.Wheels.Length; i++)
            {
                wheelAudioSources[i] = cc.Wheels[i].gameObject.GetComponent<AudioSource>();
            }
            carSource = GetComponent<AudioSource>();
        }

        public void Play(AudioSource source, AudioClip clip)
        {
            source.clip = clip;

        }
        public void PlaySlip(int i)
        {
            //foreach (var wheelAudioSource in wheelAudioSources) wheelAudioSource.Play();
            if (!wheelAudioSources[i].isPlaying) wheelAudioSources[i].Play();
        }
        public void StopSlip(int i)
        {
            //foreach (var wheelAudioSource in wheelAudioSources) wheelAudioSource.Stop();
            wheelAudioSources[i].Stop();
        }
        public void EngineSoundSpeed(float f)
        {
            carSource.pitch = f;
        }

    }
}