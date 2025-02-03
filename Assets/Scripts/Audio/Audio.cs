using System.Collections;
using GlobalSpace;
using UnityEngine;

namespace AudioSpace
{
    public class Audio : Singleton<Audio>
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private float duration = 10f;
        public void PlayAudio(AudioClip clip)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }

        public void PlayAudioFadeIn(AudioClip clip, float? maxIntensity = null)
        {
            StartCoroutine(FadeInAudio(clip, duration, maxIntensity));

        }
        private IEnumerator FadeInAudio(AudioClip clip, float duration, float? maxIntensity = null)
        {
            audioSource.clip = clip;
            audioSource.volume = 0;
            audioSource.Play();

            float targetVolume = maxIntensity ?? 1.0f; // Change this if you have a different default volume
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                audioSource.volume = Mathf.Lerp(0, targetVolume, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            audioSource.volume = targetVolume; // Ensure it reaches the target volume
        }
    }

}