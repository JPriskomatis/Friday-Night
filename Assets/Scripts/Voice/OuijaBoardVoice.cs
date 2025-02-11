using System;
using System.Collections;
using System.Collections.Generic;
using AudioSpace;
using DG.Tweening;
using GlobalSpace;
using PlayerSpace;
using UnityEngine;

namespace VoiceSpace
{
    public class OuijaBoardVoice : VoiceRecognition
    {
        public static Action OnOuijaJumpscare;

        [SerializeField] private Transform arrow;
        public Vector3 positionYES;
        public Vector3 targetPosition2 = new Vector3(0f,0f,0.0719999969f);

        

        [Header("Ouija Characters Positions")]
        [SerializeField] private string[] characters;
        [SerializeField] private Vector3[] positions;

        private Dictionary<string, Vector3> letterMap = new();

        [SerializeField] private AudioClip clip;
        

        [Header("Player answer strings")]
        [SerializeField] private string HowYouDiedText;
        [SerializeField] private string BehindYouTxt;
        [SerializeField] private string whoAreYouTxt;

        [Header("Ouija Answers")]
        [SerializeField] private string HowDidYouDieAnswer ="STRANGLED";
        [SerializeField] private string WhereAreYouAnswer = "BEHIND YOU";
        [SerializeField] private string whoAreYouAnswer = "DEATH";

        [Header("Object Behind Player")]
        [SerializeField] private GameObject behindPlayer;
        [SerializeField] private GameObject jack;


        private Camera camera;
        private bool firstClip;
        private Vector3 angry = new Vector3(-0.282000005f, 0f, -0.186000004f);

        protected override void Awake()
        {
            camera = Camera.main;

            //This maps all the letters to their corresponding position in the ouija board;
            for (int i=0; i<characters.Length; i++)
            {
                letterMap.Add(characters[i], positions[i]);
            }
            base.Start();
        }

        //TODO:
        //Call StopListening(); when the arrow moves, and StartListening(); once the arrow stops??

        public override void AddDictionaryFunctions()
        {
            //Are you here questions;
            voiceActions.Add(speechWords[0], AreYouHere);
            Debug.Log("Added: " + speechWords[0]);
            voiceActions.Add(speechWords[1], AreYouHere);
            Debug.Log("Added: " + speechWords[1]);
            voiceActions.Add(speechWords[2], AreYouHere);
            Debug.Log("Added: " + speechWords[2]);
            voiceActions.Add(speechWords[3], AreYouHere);
            Debug.Log("Added: " + speechWords[3]);

            //How did you die?
            voiceActions.Add(speechWords[4], HowDidYouDie);
            Debug.Log("Added: " + speechWords[4]);
            voiceActions.Add(speechWords[5], HowDidYouDie);
            Debug.Log("Added: " + speechWords[5]);
            voiceActions.Add(speechWords[6], HowDidYouDie);
            Debug.Log("Added: " + speechWords[6]);
            voiceActions.Add(speechWords[7], HowDidYouDie);
            Debug.Log("Added: " + speechWords[7]);

            //Where are you
            voiceActions.Add(speechWords[8], WhereAreYou);
            Debug.Log("Added: " + speechWords[8]);

            //Who are you?
            voiceActions.Add(speechWords[9], WhoAreYou);
            Debug.Log("Added: " + speechWords[9]);
            voiceActions.Add(speechWords[10], WhoAreYou);
            Debug.Log("Added: " + speechWords[10]);

        }

        private void WhoAreYou()
        {
            string answer = "BRO";
            StartCoroutine(MoveArrowToWordWithCompletion(answer, () =>
            {
                StartCoroutine(MoveToAngryAndContinue());
            }));
        }

        private IEnumerator MoveToAngryAndContinue()
        {
            yield return arrow.DOLocalMove(angry, 0.5f).SetEase(Ease.InOutQuad).WaitForCompletion();
            yield return new WaitForSeconds(1f); // Stay in angry position for 1 second

            string answer = whoAreYouAnswer;
            StartCoroutine(MoveArrowToWordWithCompletion(answer, () =>
            {
                PlayerThoughts.Instance.SetText(whoAreYouTxt);
            }));
        }

        private void WhereAreYou()
        {
            PlayAudio();
            string answer = WhereAreYouAnswer;
            Debug.Log("This actually works");

            StartCoroutine(MoveArrowToWordWithCompletion(answer, () =>
            {
                PlayerThoughts.Instance.SetText(BehindYouTxt);
            }));

            //First check if the player looks at behindPlayer;
            StartCoroutine(CheckPlayerLookingAt(behindPlayer, () =>
            {
                Debug.Log("Looked behind");
                jack.SetActive(true);

                //Now check if the player looks at Jack;
                StartCoroutine(CheckPlayerLookingAt(jack, () =>
                {
                    Debug.Log("Looked at Jack");

                    StartCoroutine(DelayForGlitch());

                }));
            }));
        }

        IEnumerator DelayForGlitch()
        {
            yield return new WaitForSeconds(0.3f);
            PlayerCamera.Instance.InitiateGlitchEffect();
            jack.SetActive(false);

            OnOuijaJumpscare?.Invoke();
            
            Destroy(this);

        }

        IEnumerator CheckPlayerLookingAt(GameObject target, System.Action onLooked)
        {
            while (true)
            {
                if (IsPlayerLookingAt(target))
                {
                    onLooked?.Invoke();
                    yield break; // Stop coroutine after detection
                }
                yield return null;
            }
        }

        private bool IsPlayerLookingAt(GameObject target)
        {
            RaycastHit hit;
            if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, 10f)) // Adjust distance if needed
            {
                if (hit.collider.gameObject == target && hit.collider.gameObject.layer == 7) // Layer 7 check
                {
                    return true;
                }
            }
            return false;
        }




        private void AreYouHere()
        {
            PlayAudio();
            Debug.Log("This actually works");
            arrow.DOLocalMove(positionYES, 2f).SetEase(Ease.InOutQuad);
        
        }
        private void HowDidYouDie()
        {
            PlayAudio();
            string answer = HowDidYouDieAnswer;
            Debug.Log("This actually works");

            // Call MoveArrowToWord and pass a callback for completion
            StartCoroutine(MoveArrowToWordWithCompletion(answer, () =>
            {
                // This will execute when the MoveArrowToWord coroutine completes
                PlayerThoughts.Instance.SetText(HowYouDiedText);
                // Trigger any additional actions you want after the movement completes
            }));
        }

        private void PlayAudio()
        {
            if (!firstClip)
            {
                Audio.Instance.PlayAudioFadeIn(clip, 0.3f);
                firstClip = true;
            }
        }
        private IEnumerator MoveArrowToWordWithCompletion(string word, Action onComplete)
        {
            // Call the existing MoveArrowToWord coroutine
            yield return StartCoroutine(MoveArrowToWord(word));

            // Call the callback after the MoveArrowToWord coroutine finishes
            onComplete?.Invoke();
        }
        private IEnumerator MoveArrowToWord(string word)
        {
            foreach (char letter in word)
            {
                string letterStr = letter.ToString().ToUpper(); // Ensure case consistency
                if (letterMap.TryGetValue(letterStr, out Vector3 targetPos))
                {
                    yield return arrow.DOLocalMove(targetPos, 0.5f).SetEase(Ease.InOutQuad).WaitForCompletion();
                    yield return new WaitForSeconds(0.5f);
                }
                else
                {
                    Debug.LogWarning($"Letter '{letterStr}' not found in dictionary!");
                }
            }
        }

    }

}