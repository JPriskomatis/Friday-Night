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

        [SerializeField] private GameObject cinemaCam;


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
            blackScreen.DOFade(1, 0.2f).OnComplete(() 
                => monster.SetActive(true));

            StartCoroutine(Delay());
            
        }
        public void RemoveBlackScreen()
        {
            blackScreen.DOFade(0, 0.2f);
        }

        IEnumerator Delay()
        {
            yield return new WaitForSeconds(1f);
            RemoveBlackScreen();

        }

    }

}