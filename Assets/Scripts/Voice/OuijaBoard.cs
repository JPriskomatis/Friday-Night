using DG.Tweening;
using UnityEngine;

namespace VoiceSpace
{
    public class OuijaBoard : VoiceRecognition
    {
        [SerializeField] private Transform arrow;
        public Vector3 positionYES;
        public Vector3 targetPosition2 = new Vector3(0f,0f,0.0719999969f);
        public override void AddDictionaryFunctions()
        {
            voiceActions.Add("Are you here", AreYouHere);
            voiceActions.Add("wait", SentenceTwo);
        }
        private void AreYouHere()
        {
            Debug.Log("This actually works");
            arrow.DOLocalMove(positionYES, 2f).SetEase(Ease.InOutQuad);
        
        }
        private void SentenceTwo()
        {
            Debug.Log("This actually works");
            arrow.DOLocalMove(targetPosition2, 2f).SetEase(Ease.InOutQuad);
        }
        
    }

}