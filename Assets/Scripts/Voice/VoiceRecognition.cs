using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Windows.Speech;
using DG.Tweening;

namespace VoiceSpace
{
    public abstract class VoiceRecognition : MonoBehaviour
    {
        private KeywordRecognizer keywordRecognizer;

        protected Dictionary<string, Action> voiceActions = new Dictionary<string, Action>();

        [SerializeField] protected string[] speechWords;

        [SerializeField] private GameObject micronhponeUI;
        [SerializeField] private CanvasGroup micronhponeCanvas;

        private void OnDisable()
        {
            micronhponeCanvas.DOFade(0, 1f).OnComplete(() => micronhponeUI.SetActive(false));
        }

        protected void Awake()
        {
            AddDictionaryFunctions();
        }
        protected virtual void Start()
        {
            //When we want to stop it;
            //keywordRecognizer.Stop();
            keywordRecognizer = new KeywordRecognizer(voiceActions.Keys.ToArray());
            keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;

            keywordRecognizer.Start();

            micronhponeUI.SetActive(true);
            micronhponeCanvas.DOFade(1, 1f);
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
