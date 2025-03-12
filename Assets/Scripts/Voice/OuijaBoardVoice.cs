using System;
using System.Collections;
using System.Collections.Generic;
using AudioSpace;
using DG.Tweening;
using GlobalSpace;
using PlayerSpace;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;

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

        [Header("Extra Components")]
        [SerializeField] GameObject key;
        [SerializeField] private AudioSource audioSource;

        public UnityEvent OnAppearNote;


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
            base.Awake();
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
            voiceActions.Add(speechWords[8], WhoAreYou);
            Debug.Log("Added: " + speechWords[8]);


            //Who are you?
            voiceActions.Add(speechWords[9], WhoAreYou);
            Debug.Log("Added: " + speechWords[9]);


            //Where are you
            voiceActions.Add(speechWords[10], WhereAreYou);
            voiceActions.Add(speechWords[11], WhereAreYou);
            voiceActions.Add(speechWords[12], WhereAreYou);
            voiceActions.Add(speechWords[13], WhereAreYou);
            voiceActions.Add(speechWords[14], WhereAreYou);
            voiceActions.Add(speechWords[15], WhereAreYou);
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

            OnAppearNote?.Invoke();

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
                    audioSource.Stop();
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
            key.SetActive(true);
            ExitVoiceAction();
            this.GetComponent<SphereCollider>().enabled = false;
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
                PlayAudioFadeIn(clip, 0.3f);
                firstClip = true;
            }
        }



        public void PlayAudioFadeIn(AudioClip clip, float? maxIntensity = null, bool? loop = null)
        {
            audioSource.loop = false;
            StartCoroutine(FadeInAudio(clip, 5f, maxIntensity));
            if (loop != null)
            {
                audioSource.loop = true;
            }
            else
            {
                audioSource.loop = false;
            }
        }


        private IEnumerator FadeInAudio(AudioClip clip, float duration, float? maxIntensity = null)
        {
            audioSource.clip = clip;
            audioSource.volume = 0;
            audioSource.Play();

            float targetVolume = maxIntensity ?? 1.0f;
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                audioSource.volume = Mathf.Lerp(0, targetVolume, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            audioSource.volume = targetVolume;
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