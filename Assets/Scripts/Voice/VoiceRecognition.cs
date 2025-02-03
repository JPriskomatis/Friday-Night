using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Windows.Speech;

namespace VoiceSpace
{
    public abstract class VoiceRecognition : MonoBehaviour
    {
        private KeywordRecognizer keywordRecognizer;

        protected Dictionary<string, Action> voiceActions = new Dictionary<string, Action>();

        [SerializeField] protected string[] speechWords;

        protected virtual void Start()
        {
            AddDictionaryFunctions();
            //When we want to stop it;
            //keywordRecognizer.Stop();
            keywordRecognizer = new KeywordRecognizer(voiceActions.Keys.ToArray());
            keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;

            keywordRecognizer.Start();
        }
        protected void StopListening()
        {
            keywordRecognizer.Stop();
        }
        public abstract void AddDictionaryFunctions();

        //This method gets the speech and return its key pair (for example if the speech is forward then it returns the function we have
        //linked the word "forward" with;
        private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
        {
            Debug.Log(speech.text);
            voiceActions[speech.text].Invoke();
        }

        private void ForwardFun()
        {
            Debug.Log("This is forward function");
        }

    }
}
