using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class ReverbScript : MonoBehaviour
{
    AudioReverbZone m_audioReverbZone;
    private void Start()
    {
        m_audioReverbZone = GetComponent<AudioReverbZone>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) m_audioReverbZone.enabled = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) m_audioReverbZone.enabled = false;
    }
}
