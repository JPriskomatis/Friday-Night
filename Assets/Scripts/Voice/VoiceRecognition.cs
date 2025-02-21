using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Windows.Speech;
using DG.Tweening;
using System.Collections;

namespace VoiceSpace
{
    public abstract class VoiceRecognition : MonoBehaviour
    {
        private KeywordRecognizer keywordRecognizer;

        protected Dictionary<string, Action> voiceActions = new Dictionary<string, Action>();

        [SerializeField] protected string[] speechWords;

        [SerializeField] private GameObject micronhponeUI;
        [SerializeField] private CanvasGroup micronhponeCanvas;

        //fire event to VoiceButtonDisplay UI;
        public static event Action<Dictionary<string, Action>> OnVoiceStart;
        public static event Action OnExitVoice;

        [SerializeField] protected int commandsButtonCount;

        private void OnDisable()
        {
            micronhponeCanvas.DOFade(0, 1f).OnComplete(() => micronhponeUI.SetActive(false));
        }

        protected virtual void Awake()
        {
            AddDictionaryFunctions();
        }
        //TODO:
        //Create a function StartListening???
        
        protected virtual void Start()
        {
            //When we want to stop it;
            //keywordRecognizer.Stop();
            keywordRecognizer = new KeywordRecognizer(voiceActions.Keys.ToArray());
            keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;

            keywordRecognizer.Start();

            micronhponeUI.SetActive(true);
            micronhponeCanvas.DOFade(1, 1f);

            OnVoiceStart?.Invoke(voiceActions);

            StartCoroutine(CheckVoiceActionIndexes());
        }
        protected void StopListening()
        {
            keywordRecognizer.Stop();
            OnExitVoice?.Invoke();


        }

        private IEnumerator CheckVoiceActionIndexes()
        { 
            while (true)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    if (voiceActions.Count > 0)
                    {
                        voiceActions.ElementAt(0).Value.Invoke();
                        Debug.Log("Invoked action for first element.");
                    }
                    if (commandsButtonCount == 1)
                    {
                        Debug.Log("Last");
                        OnExitVoice?.Invoke();
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    if (voiceActions.Count > 1)
                    {
                        int middleIndex = voiceActions.Count / 2;
                        voiceActions.ElementAt(middleIndex).Value.Invoke();
                        Debug.Log("Invoked action for middle element.");
                    }
                    if (commandsButtonCount == 2)
                    {
                        Debug.Log("Last");
                        OnExitVoice?.Invoke();
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    if (voiceActions.Count > 0)
                    {
                        voiceActions.ElementAt(voiceActions.Count - 1).Value.Invoke();
                        Debug.Log("Invoked action for last element.");
                    }
                    if (commandsButtonCount == 3)
                    {
                        Debug.Log("Last");
                        OnExitVoice?.Invoke();
                    }
                    

                }

                yield return null;
            }
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
