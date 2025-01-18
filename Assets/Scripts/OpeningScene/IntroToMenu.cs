using DG.Tweening;
using UnityEngine;

namespace OpeningScene
{
    public class IntroToMenu : MonoBehaviour
    {
        [SerializeField] private CanvasGroup ejetaLogo;
        [SerializeField] private CanvasGroup blackScreen;

        private void Start()
        {
            //Show Ejeta Logo;
            ShowLogo(ejetaLogo);
        }

        private void ShowLogo(CanvasGroup logo)
        {
            logo.DOFade(1, 3f).OnComplete(() =>
                        DOTween.Sequence().AppendInterval(2f)
                        .Append(logo.DOFade(0, 3f)).OnComplete(() =>
                        blackScreen.DOFade(0, 1f)
                        )
            );

            
        }
    }


}