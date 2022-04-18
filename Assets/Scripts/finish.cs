using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class finish : MonoBehaviour
{
    [SerializeField] GameObject guard;
    [SerializeField] GameObject scene;
    private void OnCollisionEnter(Collision collision)
    {
        if (!guard.active) SceneManager.LoadScene("FreeplayDich");
    }
    
}
