using TMPro;
using UnityEngine;

namespace Mechanics
{
    public class TextReveal : MonoBehaviour
    {
        [SerializeField] private TextMeshPro endText;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player")) endText.gameObject.SetActive(true);
        }
    }
}
