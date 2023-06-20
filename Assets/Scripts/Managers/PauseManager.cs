using UnityEngine;

namespace Managers
{
    public class PauseManager : MonoBehaviour
    {
        public static PauseManager instance;
        
        [HideInInspector] public bool isGamePaused;
        public GameObject pauseScreen;

        private void Awake()
        {
            if (instance == null) instance = this;
            else Destroy(gameObject);
        }

        private void Start()
        {
            isGamePaused = false;
            pauseScreen.SetActive(false);
        }

        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Escape)) return;
            isGamePaused = !isGamePaused;
            if (isGamePaused)
                PauseGame();
            else
                ResumeGame();
        }

        private void PauseGame()
        {
            Time.timeScale = 0f;
            pauseScreen.SetActive(true);
            
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        
        private void ResumeGame()
        {
            Time.timeScale = 1f;
            pauseScreen.SetActive(false);
            
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
