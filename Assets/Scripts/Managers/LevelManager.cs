using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Serialization;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
        public TextMeshProUGUI levelText;
        public string levelName;
        private void Start()
        {
            levelText.gameObject.SetActive(true);
            levelText.text = levelName;
            
            //fade levelText color alpha in
            var tempColor = levelText.color;
            tempColor.a = 0f;
            levelText.color = tempColor;
            
            levelText.DOFade(1f, 0.5f).OnComplete(() =>
            {
                levelText.transform.DOScale(1.5f, 1f).SetEase(Ease.OutBounce).OnComplete(() =>
                {
                    levelText.transform.DOScale(1f, 0.5f).SetEase(Ease.OutBounce).OnComplete(() =>
                    {
                        Invoke(nameof(FadeOutText), 1f);
                    });
                });
            });

        }

        private void FadeOutText()
        {
            levelText.DOFade(0f, 0.5f).OnComplete(() =>
            {
                levelText.gameObject.SetActive(false);
            });
        }
    }
}
