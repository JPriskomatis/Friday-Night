using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Windows.Speech;
using DG.Tweening;
using System.Collections;
using UISpace;
using GlobalSpace;

namespace VoiceSpace
{
    public abstract class VoiceRecognition : MonoBehaviour
    {
        private KeywordRecognizer keywordRecognizer;

        protected Dictionary<string, Action> voiceActions = new Dictionary<string, Action>();

        [SerializeField] protected string[] speechWords;

        [SerializeField] private GameObject micronhponeUI;
        [SerializeField] private CanvasGroup micronhponeCanvas;

        [SerializeField] private MicrophoneSelect microphoneSelect;
        private string selectedMic;


        //fire event to VoiceButtonDisplay UI;



        protected virtual void Awake()
        {
            //AddDictionaryFunctions();

        }
        //TODO:
        //Create a function StartListening???

        private void OnEnable()
        {
            micronhponeUI.SetActive(true);
            micronhponeCanvas.DOFade(1, 1f);
        }

        private void OnDisable()
        {
            micronhponeCanvas.DOFade(0, 1f).OnComplete(
                () => micronhponeUI.SetActive(false));
        }


        protected virtual void Start()
        {

            selectedMic = microphoneSelect.microphone; // Get the selected microphone from the ScriptableObject

            if (string.IsNullOrEmpty(selectedMic) || !Microphone.devices.Contains(selectedMic))
            {
                selectedMic = Microphone.devices.Length > 0 ? Microphone.devices[0] : null;
            }

            Debug.Log($"Using Microphone: {selectedMic}");



            AddDictionaryFunctions();

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

        protected void ExitVoiceAction()
        {
            micronhponeCanvas.DOFade(0, 1f).OnComplete(
                () => micronhponeUI.SetActive(false));

            HintMessage.Instance.RemoveMessage();
            PlayerController.Instance.ResetMovement();
            this.GetComponent<SphereCollider>().enabled = false;
        }


        public abstract void AddDictionaryFunctions();

        //This method gets the speech and return its key pair (for example if the speech is forward then it returns the function we have
        //linked the word "forward" with;
        private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
        {
            Debug.Log(speech.text);
            voiceActions[speech.text].Invoke();
        }
    }
}
