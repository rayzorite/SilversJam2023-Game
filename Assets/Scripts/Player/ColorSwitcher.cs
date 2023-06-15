using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class ColorSwitcher : MonoBehaviour
    {
        public static ColorSwitcher Instance;
        
        [SerializeField] private Image colorImage;
        public Color redColor;
        public Color blueColor;
        public ParticleSystem colorSwitchParticle;

        [HideInInspector] public bool isRedColor;
        [HideInInspector] public bool isBlueColor;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Red"))
            {
                isRedColor = true;
                isBlueColor = false;
            }
            else if (other.CompareTag("Blue"))
            {
                isBlueColor = true;
                isRedColor = false;
            }
            
            if (isRedColor) SwitchColor(redColor);
            else if (isBlueColor) SwitchColor(blueColor);
        }

        private void SwitchColor(Color color)
        {
            DOTween.To(() => colorImage.color, x => colorImage.color = x, color, 0.35f);
        }
    }
}
