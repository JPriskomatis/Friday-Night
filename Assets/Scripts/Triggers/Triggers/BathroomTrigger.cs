using AudioSpace;
using MonsterSpace;
using ObjectSpace;
using PlayerSpace;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

namespace TriggerSpace
{
    public class BathroomTrigger : FloorTrigger
    {
        [SerializeField] private AudioSource source;

        [SerializeField] private Door door;
        [SerializeField] Flashlight flashlight;


        protected override void InitiateAction()
        {
            //Slam the door;
            interactAgain = false;
            door.PublicCloseDoor();
            StartCoroutine(IncreaseAudio());
            FlickeringEffect();

        }

        private void FlickeringEffect()
        {
            flashlight.StartFlicker();
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