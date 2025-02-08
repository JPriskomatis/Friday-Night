using AISpace;
using DG.Tweening;
using System.Collections;
using UnityEngine;
using VoiceSpace;

namespace MonsterSpace
{
    public class Jumpscare : MonoBehaviour
    {
        [Header("Material Settings")]
        [SerializeField] private Material mat;
        [SerializeField] private float noiseAmount;
        [SerializeField] private float glitchStrength;
        [SerializeField] private float scanlinesStrength;

        [Header("Jumpscare Components")]
        [SerializeField] private CanvasGroup blackScreen;
        [SerializeField] private GameObject monster;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private GameObject cinemaCam;

        //Subscribe to events;

        private void OnEnable()
        {
            MonsterAI.OnPlayerCapture += InitiateJumpscare;
            
        }
        private void OnDisable()
        {
            MonsterAI.OnPlayerCapture -= InitiateJumpscare;
        }
        private void Start()
        {
            mat.SetFloat("_NoiseAmount", 0);
            mat.SetFloat("_GlitchStrength", 0);
            mat.SetFloat("_ScanLinesStrength", 1);
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                InitiateJumpscare();
            }
            
        }
        public void InitiateJumpscare()
        {
            StartCoroutine(StartGlitchEffect());
            StartCoroutine(ShowcaseBlackScreen());
        }
        IEnumerator StartGlitchEffect()
        {
            float currentNoiseAmount = mat.GetFloat("_NoiseAmount");
            DOTween.To(() => currentNoiseAmount, x =>
            {
                currentNoiseAmount = x;
                mat.SetFloat("_NoiseAmount", currentNoiseAmount);
            }, noiseAmount, 1f);

            float currentGlitchStrength = mat.GetFloat("_GlitchStrength");
            DOTween.To(() => currentGlitchStrength, x =>
            {
                currentGlitchStrength = x;
                mat.SetFloat("_GlitchStrength", currentGlitchStrength);
            }, glitchStrength, 1f);

            float currentScanlinesStrength = mat.GetFloat("_ScanLinesStrength");
            DOTween.To(() => currentScanlinesStrength, x =>
            {
                currentScanlinesStrength = x;
                mat.SetFloat("_ScanLinesStrength", currentScanlinesStrength);
            }, scanlinesStrength, 1f);

            yield return new WaitForSeconds(0.5f);
            StopController();
        }


        public void StopController()
        {
            PlayerController.Instance.StopMovement();
            PlayerController.Instance.DisableCameraMovement();
            cinemaCam.SetActive(false);
        }

        IEnumerator ShowcaseBlackScreen()
        {
            yield return new WaitForSeconds(0.5f);

            blackScreen.DOFade(1, 0f).OnComplete(() =>
            {
                //We want to "reset" the camera so that if the player is looking down our monster don't glitch from the rotation;
                Camera.main.transform.localRotation = Quaternion.identity;
                monster.SetActive(true);
            });

            StartCoroutine(Delay());
        }
        public void RemoveBlackScreen()
        {
            blackScreen.DOFade(0, 0.05f);
            audioSource.Play();

            StartCoroutine(RemoveJumpscare());

        }

        IEnumerator Delay()
        {
            yield return new WaitForSeconds(1f);
            RemoveBlackScreen();

        }

        IEnumerator RemoveJumpscare()
        {
            yield return new WaitForSeconds(1f);
            blackScreen.DOFade(1, 0f);
            monster.SetActive(false);

            mat.SetFloat("_NoiseAmount", 0);
            mat.SetFloat("_GlitchStrength", 0);
            mat.SetFloat("_ScanLinesStrength", 1);

            //Reset player control;
            PlayerController.Instance.EnableCaneraMovement();
            PlayerController.Instance.ResetMovement();

            yield return new WaitForSeconds(0.5f);
            blackScreen.DOFade(0, 0.2f);
            cinemaCam.SetActive(true);
        }

    }

}