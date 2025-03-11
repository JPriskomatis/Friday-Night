using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Windows.Speech;
using DG.Tweening;
using System.Collections;
using UISpace;
using GlobalSpace;
using UnityEngine.Audio;

namespace VoiceSpace
{
    public abstract class VoiceRecognition : MonoBehaviour
    {
        private DictationRecognizer dictationRecognizer;

        protected Dictionary<string, Action> voiceActions = new Dictionary<string, Action>();

        [SerializeField] protected string[] speechWords;

        [SerializeField] private GameObject micronhponeUI;
        [SerializeField] private CanvasGroup micronhponeCanvas;

        [SerializeField] private MicrophoneSelect microphoneSelect;
        private string selectedMic;


        //fire event to VoiceButtonDisplay UI;

        protected virtual void Awake()
        {
            // AddDictionaryFunctions();
        }

        private void OnEnable()
        {
            if (dictationRecognizer == null)
            {
                dictationRecognizer = new DictationRecognizer();

                // Reattach event listeners
                dictationRecognizer.DictationResult += RecognizedSpeech;
                dictationRecognizer.DictationComplete += DictationComplete;
                dictationRecognizer.DictationError += DictationError;
            }

            dictationRecognizer.Start();
            micronhponeUI.SetActive(true);
            micronhponeCanvas.DOFade(1, 1f);
        }


        private void OnDisable()
        {
            dictationRecognizer.Stop();
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

            
        }

        protected void StopListening()
        {
            dictationRecognizer.Stop();
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

        // This method gets the speech and returns its key pair
        private void RecognizedSpeech(string speech, ConfidenceLevel confidence)
        {
            speech = speech.ToLower().Replace("?", "");
            Debug.Log(speech);
            if (voiceActions.ContainsKey(speech))
            {
                voiceActions[speech].Invoke();
            }
        }

        // Called when dictation is complete (user stops talking)
        private void DictationComplete(DictationCompletionCause cause)
        {
            Debug.Log("Dictation complete: " + cause);
            // Optionally handle completion events here
        }

        // Called when dictation encounters an error
        private void DictationError(string error, int hresult)
        {
            Debug.LogError($"Dictation error: {error}, hresult: {hresult}");
            // Optionally handle error events here
        }
    }
}
