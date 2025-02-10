using AudioSpace;
using ObjectSpace;
using PlayerSpace;
using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Audio;

namespace VoiceSpace
{
    public class BathroomMirrorVOICE : VoiceRecognition
    {
        [SerializeField] private Material dissolveMat;
        [SerializeField] private float duration;
        [SerializeField] private Component bloodAppear;
        [SerializeField] private AudioClip clip;

        [Header("Extra Components")]
        [SerializeField] private Door door;
        [SerializeField] private AudioSource source;

        public static event Action OnFlickering;
        public static event Action OnStopMirror;



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
            StartCoroutine(StopEffects());

        }

        IEnumerator StopEffects()
        {
            yield return new WaitForSeconds(3f);

            float startVolume = source.volume;
            float timeElapsed = 0f;

            while (timeElapsed < 3)
            {
                source.volume = Mathf.Lerp(startVolume, 0, timeElapsed / 3);
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            source.volume = 0;
            source.Stop();
            Audio.Instance.FadeOut();
            OnStopMirror?.Invoke();
            source.gameObject.SetActive(false);

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