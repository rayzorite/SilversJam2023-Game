using System;
using Player;
using UnityEngine;
using UnityEngine.Serialization;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        
        public GameObject[] redBlocks;
        public GameObject[] blueBlocks;
        public AudioSource pickupSound;
        [HideInInspector] public Renderer[] redBlockRenderers;
        [HideInInspector] public Collider[] redBlockColliders;
        [HideInInspector] public Renderer[] blueBlockRenderers;
        [HideInInspector] public Collider[] blueBlockColliders;

        private void Awake()
        {
            if (instance == null) instance = this;
            else Destroy(gameObject);
        }

        private void Start()
        {
            redBlockRenderers = new Renderer[redBlocks.Length];
            redBlockColliders = new Collider[redBlocks.Length];
            blueBlockRenderers = new Renderer[blueBlocks.Length];
            blueBlockColliders = new Collider[blueBlocks.Length];

            for (int i = 0; i < redBlocks.Length; i++)
            {
                redBlockRenderers[i] = redBlocks[i].GetComponent<Renderer>();
                redBlockColliders[i] = redBlocks[i].GetComponent<Collider>();
            
                redBlockColliders[i].enabled = false;

                var tempColor = redBlockRenderers[i].material.color;
                tempColor.a = 0;
                redBlockRenderers[i].material.color = tempColor;
            }

            for (int i = 0; i < blueBlocks.Length; i++)
            {
                blueBlockRenderers[i] = blueBlocks[i].GetComponent<Renderer>();
                blueBlockColliders[i] = blueBlocks[i].GetComponent<Collider>();
            
                blueBlockColliders[i].enabled = false;

                var tempColor = blueBlockRenderers[i].material.color;
                tempColor.a = 0f;
                blueBlockRenderers[i].material.color = tempColor;
            }
        }

        private void Update()
        {
            if (ColorSwitcher.instance.isRedColor)
            {
                for (int i = 0; i < redBlocks.Length; i++)
                {
                    redBlockColliders[i].enabled = true;
                    redBlockRenderers[i].material.color = Color.white;
                }

                for (int i = 0; i < blueBlocks.Length; i++)
                {
                    blueBlockColliders[i].enabled = false;

                    var tempColor = blueBlockRenderers[i].material.color;
                    tempColor.a = 0f;
                    blueBlockRenderers[i].material.color = tempColor;
                }
            }

            if (ColorSwitcher.instance.isBlueColor)
            {
                for (int i = 0; i < blueBlocks.Length; i++)
                {
                    blueBlockColliders[i].enabled = true;
                    blueBlockRenderers[i].material.color = Color.white;
                }

                for (int i = 0; i < redBlocks.Length; i++)
                {
                    redBlockColliders[i].enabled = false;

                    var tempColor = redBlockRenderers[i].material.color;
                    tempColor.a = 0f;
                    redBlockRenderers[i].material.color = tempColor;
                }
            }
        }
    }
}
