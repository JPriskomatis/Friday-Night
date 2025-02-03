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

        private void OnEnable()
        {
            BathroomMirrorVOICE.OnFlickering += StartFlicker;
        }

        private void OnDisable()
        {
            BathroomMirrorVOICE.OnFlickering -= StartFlicker;
        }
        void Start()
        {
            flashlight = GetComponent<Light>();
            if (flashlight != null)
            {
                originalIntensity = flashlight.intensity;
            }
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.V))
            {
                StartFlicker();
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
    }

}