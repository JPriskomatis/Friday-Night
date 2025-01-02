using DG.Tweening;
using GlobalSpace;
using TMPro;
using UnityEngine;

namespace UISpace
{
    public class VoiceRecUI : Singleton<VoiceRecUI>
    {
        [SerializeField] private TextMeshProUGUI hintMessage;
        [SerializeField] private CanvasGroup messageCanvasGroup;

        private void Start()
        {
            messageCanvasGroup.alpha = 0f;

        }
        public void SetMessage(string message)
        {
            hintMessage.text = message;
            messageCanvasGroup.DOFade(1f, 2);
        }
        public void RemoveMessage()
        {
            messageCanvasGroup.DOFade(0f, 1);
        }
    }

}