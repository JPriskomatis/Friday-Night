using UnityEngine;
using UnityEngine.UI;

namespace PlayerSpace
{
    [RequireComponent(typeof(AudioSource))]
    public class VoiceVolume : MonoBehaviour
    {
        [SerializeField] private AudioSource source;
        [SerializeField] private Slider volumeSlider;
        [SerializeField] private VoiceDetection detector;

        [SerializeField] private float smoothingFactor = 0.1f; // Controls the smoothing (0 = no change, 1 = instant change)
        private float smoothedLoudness = 0;

        private void Start()
        {
            foreach (var device in Microphone.devices)
            {
                Debug.Log("Available Mic: " + device);
            }

            source = GetComponent<AudioSource>();
            source.clip = Microphone.Start(Microphone.devices[0], true, 20, 44100);

            source.loop = true;
            source.volume = 0;
            source.Play();
        }

        private void Update()
        {
            float currentLoudness = detector.GetMicVolume(source.timeSamples, source.clip);

            // Smooth the loudness using a moving average
            smoothedLoudness = Mathf.Lerp(smoothedLoudness, currentLoudness, smoothingFactor);

            Debug.Log($"Raw Loudness: {currentLoudness}, Smoothed Loudness: {smoothedLoudness}");

            // Update the slider with the smoothed value
            volumeSlider.value = Mathf.Clamp01(smoothedLoudness);
        }
    }
}
