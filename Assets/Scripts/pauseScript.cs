using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game
{
    public class pauseScript : MonoBehaviour
    {
        [SerializeField] GameObject menu;
        [SerializeField] Slider settings;
        bool paused = false;

        private void Update()
        {
            if ((Input.GetKeyUp(KeyCode.Escape)))
            {
                Switch();
            }
        }
        public void Switch()
        {
            paused = !paused;
            menu.SetActive(!menu.active);
            Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        }
        public void MainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }   
        public void Play()
        {
            SceneManager.LoadScene("World1");
        }
        public void FreePlay()
        {
            SceneManager.LoadScene("FreeplayDich");
        }
        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Time.timeScale = 1;
        }
        public void Settings()
        {

            settings.value *= -1;
        }
        public void Exit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}