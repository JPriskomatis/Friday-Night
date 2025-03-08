using DG.Tweening;
using UnityEngine;
using VoiceSpace;

namespace ObjectSpace
{
    public class Flashlight : MonoBehaviour
    {
        [SerializeField] private Light flashlight;
        private float originalIntensity;
        private float flickerDuration = 4f;
        public bool isPulsingStopped = false;

        private void OnEnable()
        {
            BathroomMirrorVOICE.OnStopFlashlightPulse += StopPulsing;
            BathroomMirrorVOICE.OnFlickering += StartFlicker;
            FinalPainting.OnFlicker += StartFlicker;
        }

        private void OnDisable()
        {
            BathroomMirrorVOICE.OnStopFlashlightPulse -= StopPulsing;
            BathroomMirrorVOICE.OnFlickering -= StartFlicker;
            FinalPainting.OnFlicker -= StartFlicker;
        }

        void Start()
        {
            flashlight = GetComponent<Light>();
            if (flashlight != null)
            {
                originalIntensity = flashlight.intensity;
            }
        }

        public void StartFlicker()
        {
            if (flashlight == null) return;

            Sequence flickerSequence = DOTween.Sequence();

            for (float t = 0; t < flickerDuration; t += Random.Range(0.05f, 0.2f))
            {
                flickerSequence.AppendCallback(() =>
                {
                    flashlight.enabled = Random.value > 0.3f;
                    flashlight.intensity = flashlight.enabled ? Random.Range(0.1f, originalIntensity) : 0;
                });
                flickerSequence.AppendInterval(Random.Range(0.05f, 0.2f));
            }

            flickerSequence.AppendCallback(() =>
            {
                flashlight.enabled = true;
                flashlight.intensity = originalIntensity;
            });
        }

        public void StartPulsing()
        {
            if (flashlight == null) return;
            isPulsingStopped = false;
            float currentIntensity = flashlight.intensity;

            void Pulse()
            {
                if (isPulsingStopped) return;

                flashlight.DOIntensity(2f, 0.7f).OnComplete(() =>
                {
                    if (isPulsingStopped) return;
                    flashlight.DOIntensity(currentIntensity, 0.7f).OnComplete(Pulse);
                });
            }

            Pulse();
        }

        public void StopPulsing()
        {
            isPulsingStopped = true;
        }
    }
}
