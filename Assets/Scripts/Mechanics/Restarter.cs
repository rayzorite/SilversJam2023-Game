using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mechanics
{
    public class Restarter : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
