using System;
using System.Collections;
using UnityEngine;

namespace OpeningScene
{
    public class LightFlicker : MonoBehaviour
    {
        public static event Action OnSpawnJack;

        private Light pointLight;
        public float minIntensity = 0f;
        public float maxIntensity = 0.1f;
        public float speed = 2f; // Controls how fast the intensity changes

        private bool stopFlicker;
        bool canSpawn = true;
        [SerializeField] private float randomNumThreshold;
        private void OnEnable()
        {
            OpeningMenu.OnClick += StopFlickering;
        }

        private void OnDisable()
        {
            OpeningMenu.OnClick -= StopFlickering;
        }

        private void Start()
        {
            pointLight = GetComponent<Light>();
            if (pointLight == null || pointLight.type != LightType.Point)
            {
                Debug.LogError("No Point Light component found on this GameObject!");
            }
        }

        private void Update()
        {
            if (!stopFlicker)
            {
                if (pointLight != null)
                {
                    // Use a repeating cycle with a non-linear curve for a custom timing bias
                    float cycle = Mathf.Repeat(Time.time * speed, 1f);

                    // Use a custom curve for more time in the upper half
                    float weightedCycle = Mathf.SmoothStep(0f, 1f, cycle);
                    if (cycle < 0.5f)
                    {
                        weightedCycle = Mathf.SmoothStep(0f, 1f, cycle * 2);  // Faster rise to max
                    }
                    else
                    {
                        weightedCycle = Mathf.SmoothStep(1f, 0f, (cycle - 0.5f) * 2);  // Slower fall to min
                    }

                    // Interpolate between min and max intensity
                    pointLight.intensity = Mathf.Lerp(minIntensity, maxIntensity, weightedCycle);

                    // Check for light intensity at 0.09f with a tolerance
                    if (Mathf.Abs(pointLight.intensity - 0.01f) < 0.01f)
                    {
                        TryInvokeSpawnJack();
                    }
                }
            }
            else
            {
                pointLight.intensity = Mathf.Lerp(pointLight.intensity, maxIntensity, 1f * Time.deltaTime);
            }
        }

        private void TryInvokeSpawnJack()
        {
            

            float randomNum = UnityEngine.Random.value;
            Debug.Log(randomNum);
            //20% chance to invoke the event

            if (randomNum <= randomNumThreshold && canSpawn)
            {
                canSpawn = false;
                OnSpawnJack?.Invoke();
                StartCoroutine(DelayForSpawn());
            }
        }
        IEnumerator DelayForSpawn()
        {
            yield return new WaitForSeconds(2f);
            canSpawn = true;
        }

        IEnumerator DelayForFlicker()
        {
            yield return new WaitForSeconds(1f);
            stopFlicker = true;
        }

        private void StopFlickering()
        {
            StartCoroutine(DelayForFlicker());
        }
    }
}
