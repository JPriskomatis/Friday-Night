using DG.Tweening;
using GlobalSpace;
using TMPro;
using UnityEngine;

namespace UISpace
{
    public class HintMessage : Singleton<HintMessage>
    {
        [SerializeField] private TextMeshProUGUI hintMessage;
        [SerializeField] private CanvasGroup messageCanvasGroup;

        private void Start()
        {
            messageCanvasGroup.alpha = 0f;

        }
        public void SetMessage(string message)
        {
            messageCanvasGroup.DOKill();
            hintMessage.text = message;
            messageCanvasGroup.DOFade(1f, 2).OnComplete(
                ()=>messageCanvasGroup.alpha =1);
        }
        public void RemoveMessage()
        {
            messageCanvasGroup.DOKill();
            messageCanvasGroup.DOFade(0f, 1).OnComplete(
                () => messageCanvasGroup.alpha = 0);
        }
    }

}