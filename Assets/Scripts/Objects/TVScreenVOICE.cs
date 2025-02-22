using PlayerSpace;
using UISpace;
using UnityEngine;
using UnityEngine.Video;

namespace VoiceSpace
{
    public class TVScreenVOICE : VoiceRecognition
    {
        [SerializeField] private VideoPlayer videoPlayer;
        [SerializeField] private VideoClip videoClip;
        public override void AddDictionaryFunctions()
        {
            voiceActions.Add(speechWords[0], WhatDoYouWant);
        }
        private void WhatDoYouWant()
        {
            Debug.Log("I tell you what I want");
            videoPlayer.clip = videoClip;
        }

    }

}