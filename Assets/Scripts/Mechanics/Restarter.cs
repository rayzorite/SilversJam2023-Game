using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mechanics
{
    public class Restarter : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                // Reset player position
                other.transform.position = new Vector3(0, 1.5f, 0);
            }
        }
    }
}
