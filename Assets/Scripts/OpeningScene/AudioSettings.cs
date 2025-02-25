using System.Collections;
using UnityEngine;

public class AudioSettings : MonoBehaviour
{
    public AudioSource audioSource;
    public float fadeDuration = 5f;

    void Start()
    {
        if (audioSource != null)
        {
            audioSource.volume = 0f;
            audioSource.Play();
            StartCoroutine(FadeInAudio());
        }
    }

    IEnumerator FadeInAudio()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            audioSource.volume = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        audioSource.volume = 1f; // Ensure it's fully set
    }

    public void AudioFadeOut()
    {
        StartCoroutine(FadeOutAudio());
    }
    IEnumerator FadeOutAudio()
    {
        float elapsedTime = 0f;
        float startVolume = audioSource.volume;

        while (elapsedTime < 3f)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0f, elapsedTime / 3f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        audioSource.volume = 0f; // Ensure it's fully silent

    }



}