using DG.Tweening;
using System.Collections;
using UnityEngine;

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
                StopController();
                ShowcaseBlackScreen();
                StartGlitchEffect();
            }
            
        }
        public void StartGlitchEffect()
        {
            mat.SetFloat("_NoiseAmount", noiseAmount);
            mat.SetFloat("_GlitchStrength", glitchStrength);
            mat.SetFloat("_ScanLinesStrength", scanlinesStrength);
        }

        public void StopController()
        {
            PlayerController.Instance.StopMovement();
            PlayerController.Instance.DisableCameraMovement();
            cinemaCam.SetActive(false);
        }

        public void ShowcaseBlackScreen()
        {

            blackScreen.DOFade(1, 0f).OnComplete(() =>
            {
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
        }

    }

}