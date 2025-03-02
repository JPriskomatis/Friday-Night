using AudioSpace;
using DG.Tweening.Core.Easing;
using MonsterSpace;
using ObjectSpace;
using PlayerSpace;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using VoiceSpace;

namespace TriggerSpace
{
    public class BathroomTrigger : FloorTrigger
    {
        [SerializeField] private AudioSource source, source2;
        [SerializeField] private AudioClip clip;
        [SerializeField] private Flashlight flash;

        [SerializeField] private Door door;

        private void OnEnable()
        {
            BathroomMirrorVOICE.OnStopMirror += DestroyObject;
        }

        private void OnDisable()
        {
            BathroomMirrorVOICE.OnStopMirror -= DestroyObject;
        }

        protected override void InitiateAction()
        {
            //Slam the door;
            interactAgain = false;
            FlickeringEffect();
            door.PublicCloseDoor();
            StartCoroutine(IncreaseAudio());
            

        }

        private void DestroyObject()
        {
            source2.Stop();
            Destroy(this.gameObject);
        }

        private void FlickeringEffect()
        {
            flash.StartPulsing();
            StartCoroutine(FadeInAudio(source2, 2f));
        }

        private IEnumerator FadeInAudio(AudioSource audioSource, float duration)
        {
            audioSource.volume = 0f; // Start at volume 0
            audioSource.Play();

            float elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                audioSource.volume = Mathf.Lerp(0f, 0.25f, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            audioSource.volume = 0.25f; // Ensure volume reaches 1
        }


        IEnumerator IncreaseAudio()
        {
            float targetVolume = 1.0f; // Full volume
            float startVolume = source.volume;
            float timeElapsed = 0f;
            float duration = 3f; // 3 seconds

            source.Play(); // Ensure the audio starts playing

            while (timeElapsed < duration)
            {
                source.volume = Mathf.Lerp(startVolume, targetVolume, timeElapsed / duration);
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            source.volume = targetVolume; 
        }



    }

}