using DG.Tweening;
using Managers;
using Player;
using TMPro;
using UnityEngine;

namespace Mechanics
{
    public class TextReveal : MonoBehaviour
    {
        [SerializeField] private TextMeshPro endText;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                ColorSwitcher.instance.isRedColor = false;
                ColorSwitcher.instance.isBlueColor = false;
                SwitchColor(ColorSwitcher.instance.neutralColor);
                endText.gameObject.SetActive(true);

                #region Blocks

                for (int i = 0; i < GameManager.instance.redBlocks.Length; i++)
                {
                    GameManager.instance.redBlockRenderers[i] = GameManager.instance.redBlocks[i].GetComponent<Renderer>();
                    GameManager.instance.redBlockColliders[i] = GameManager.instance.redBlocks[i].GetComponent<Collider>();
            
                    GameManager.instance.redBlockColliders[i].enabled = false;

                    var tempColor = GameManager.instance.redBlockRenderers[i].material.color;
                    tempColor.a = 0;
                    GameManager.instance.redBlockRenderers[i].material.color = tempColor;
                }
                for (int i = 0; i < GameManager.instance.blueBlocks.Length; i++)
                {
                    GameManager.instance.blueBlockRenderers[i] = GameManager.instance.blueBlocks[i].GetComponent<Renderer>();
                    GameManager.instance.blueBlockColliders[i] = GameManager.instance.blueBlocks[i].GetComponent<Collider>();
            
                    GameManager.instance.blueBlockColliders[i].enabled = false;

                    var tempColor = GameManager.instance.blueBlockRenderers[i].material.color;
                    tempColor.a = 0f;
                    GameManager.instance.blueBlockRenderers[i].material.color = tempColor;
                }

                #endregion
            }
        }
        
        private void SwitchColor(Color color)
        {
            DOTween.To(() => ColorSwitcher.instance.colorImage.color, x => ColorSwitcher.instance.colorImage.color = x, color, 0.35f);
        }
    }
}
