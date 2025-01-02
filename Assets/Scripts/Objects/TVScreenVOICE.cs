using PlayerSpace;
using UISpace;
using UnityEngine;

namespace VoiceSpace
{
    public class TVScreenVOICE : VoiceRecognition
    {

        public override void AddDictionaryFunctions()
        {
            voiceActions.Add("What do you want", WhatDoYouWant);
        }
        private void WhatDoYouWant()
        {
            Debug.Log("I tell you what I want");
        }

    }

}