using System.Collections;
using GlobalSpace;
using UnityEngine;

namespace AudioSpace
{
    public class Audio : Singleton<Audio>
    {
        public AudioSource audioSource;
        [SerializeField] private float duration = 5f;

        public void PlayAudio(AudioClip clip)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }

        public void PlayAudioFadeIn(AudioClip clip, float? maxIntensity = null, bool? loop = null)
        {
            StartCoroutine(FadeInAudio(clip, duration, maxIntensity));
            if (loop!=null)
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

        public void FadeOut()
        {
            StartCoroutine(FadeOutAudio(duration));
        }

        private IEnumerator FadeOutAudio(float duration)
        {
            float startVolume = audioSource.volume;
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                audioSource.volume = Mathf.Lerp(startVolume, 0, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            audioSource.volume = 0; // Ensure it reaches zero
            audioSource.Stop();
        }
    }
}