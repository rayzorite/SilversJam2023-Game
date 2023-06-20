using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class MenuManager : MonoBehaviour
    {
        private void Start()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 1f;
        }
        
        public void PlayGame()
        {
            Invoke(nameof(ActuallyStartsTheGame), 0.25f);
        }

        private void ActuallyStartsTheGame()
        {
            SceneManager.LoadSceneAsync(1);
        }
        
        public void QuitGame()
        {
            Invoke(nameof(ActuallyQuitsTheGame), 0.25f);
        }
        
        private void ActuallyQuitsTheGame()
        {
            Application.Quit();
        }
    }
}
