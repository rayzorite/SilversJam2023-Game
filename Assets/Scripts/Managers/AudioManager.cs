using UnityEngine;

namespace Managers
{
    public class AudioManager : MonoBehaviour
    {
        private void Awake()
        {
            SetupMusic();
        }
        
        private void SetupMusic()
        {
            if (FindObjectsOfType(GetType()).Length > 1)
                Destroy(gameObject);
            else
                DontDestroyOnLoad(gameObject);
        }
    }
}
