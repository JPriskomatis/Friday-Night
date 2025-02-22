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

        public static event Action OnOpenBookcase;

        public override void AddDictionaryFunctions()
        {
            voiceActions.Add(speechWords[0], WhatDoYouWant);
        }
        private void WhatDoYouWant()
        {
            Debug.Log("I tell you what I want");
            videoPlayer.clip = videoClip;

            OnAnimationBegin?.Invoke();
            this.GetComponent<SphereCollider>().enabled = false;

            audioSource.Play();
            Audio.Instance.PlayAudioFadeIn(clip);

            PlayerController.Instance.ResetMovement();
            HintMessage.Instance.RemoveMessage();

            StartCoroutine(DelayForBookscase());
        }

        IEnumerator DelayForBookscase()
        {
            yield return new WaitForSeconds(2.5f);
            OnOpenBookcase?.Invoke();
            this.enabled = false;
        }

    }

}