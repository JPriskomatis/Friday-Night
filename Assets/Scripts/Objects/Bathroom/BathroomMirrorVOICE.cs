using AudioSpace;
using ObjectSpace;
using PlayerSpace;
using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace VoiceSpace
{
    public class BathroomMirrorVOICE : VoiceRecognition
    {
        [SerializeField] private Material dissolveMat;
        [SerializeField] private float duration;
        [SerializeField] private Component bloodAppear;
        [SerializeField] private AudioClip clip;

        [SerializeField] private Door door;


        public static event Action OnFlickering;



        private bool hasAppeared;
        private void Awake()
        {
            dissolveMat.SetFloat("_DissolveStrength", 1f);
        }

        public override void AddDictionaryFunctions()
        {
            voiceActions.Add(speechWords[0], AreYouHere);
            voiceActions.Add(speechWords[1], AreYouHere);
            voiceActions.Add(speechWords[2], AreYouHere);
            voiceActions.Add(speechWords[3], AreYouHere);
        }

        private void AreYouHere()
        {
            if (!hasAppeared)
            {
                StartCoroutine(Dissolve(1f, 0.25f, duration));
                OnFlickering?.Invoke();
            }
            UnityEngine.Debug.Log("I will appear now");
            door.canOpen = true;

        }

        private IEnumerator Dissolve(float startValue, float endValue, float duration)
        {


            Audio.Instance.PlayAudioFadeIn(clip);
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