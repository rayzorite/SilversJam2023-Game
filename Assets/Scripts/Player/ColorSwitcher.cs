using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class ColorSwitcher : MonoBehaviour
    {
        public static ColorSwitcher instance;
        
        public Image colorImage;
        public Color neutralColor;
        public Color redColor;
        public Color blueColor;
        public ParticleSystem colorSwitchParticle;

        [HideInInspector] public bool isRedColor;
        [HideInInspector] public bool isBlueColor;

        private void Awake()
        {
            if (instance == null) instance = this;
            else Destroy(gameObject);
        }

        private void Start()
        {
            isRedColor = false;
            isBlueColor = false;
            SwitchColor(neutralColor);
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
