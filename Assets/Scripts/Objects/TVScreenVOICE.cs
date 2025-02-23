using System;
using System.Collections;
using AudioSpace;
using PlayerSpace;
using UISpace;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

namespace VoiceSpace
{
    public class TVScreenVOICE : VoiceRecognition
    {
        [SerializeField] private VideoPlayer videoPlayer;
        [SerializeField] private VideoClip videoClip;
        [SerializeField] private UnityEvent OnAnimationBegin;
        
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip clip;
        [SerializeField] private GameObject suddenGlitch;
        public static event Action OnOpenBookcase;

        public override void AddDictionaryFunctions()
        {
            voiceActions.Add(speechWords[0], WhatDoYouWant);
            //voiceActions.Add(speechWords[1], WhatDoYouWant);
            //voiceActions.Add(speechWords[2], WhatDoYouWant);
        }
        private void WhatDoYouWant()
        {
            Debug.Log("I tell you what I want");
            StartCoroutine(StartGlitchEffect());

            videoPlayer.clip = videoClip;
            videoPlayer.Play();

            OnAnimationBegin?.Invoke();
            this.GetComponent<SphereCollider>().enabled = false;

            audioSource.Play();
            Audio.Instance.PlayAudioFadeIn(clip);

            PlayerController.Instance.ResetMovement();
            HintMessage.Instance.RemoveMessage();

            StartCoroutine(DelayForBookscase());
        }
        IEnumerator StartGlitchEffect()
        {
            suddenGlitch.SetActive(true);
            yield return new WaitForSeconds(2f);
            suddenGlitch.SetActive(false);
        }

        IEnumerator DelayForBookscase()
        {
            yield return new WaitForSeconds(2.5f);
            OnOpenBookcase?.Invoke();
            this.enabled = false;
        }

    }

}