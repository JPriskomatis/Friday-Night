using PlayerSpace;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace VoiceSpace
{
    public class BathroomMirror : VoiceRecognition
    {
        [SerializeField] private Material dissolveMat;
        [SerializeField] private float duration;
        [SerializeField] private Component bloodAppear;

        private void Awake()
        {
            dissolveMat.SetFloat("_DissolveStrength", 1f);
        }
        public override void AddDictionaryFunctions()
        {
            voiceActions.Add("Appear", AreYouHere);
        }

        private void AreYouHere()
        {
            UnityEngine.Debug.Log("I will appear now");
            StartCoroutine(Dissolve(1f, 0.1f, duration));
        }

        private IEnumerator Dissolve(float startValue, float endValue, float duration)
        {
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float currentValue = Mathf.Lerp(startValue, endValue, elapsedTime / duration);
                dissolveMat.SetFloat("_DissolveStrength", currentValue);
                yield return null;
            }

            // Ensure final value is set
            dissolveMat.SetFloat("_DissolveStrength", endValue);
        }

 
    }

}