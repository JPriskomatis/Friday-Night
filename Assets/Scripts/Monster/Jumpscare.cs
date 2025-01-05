using DG.Tweening;
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

        private void Update()
        {
            StartGlitchEffect();
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
        }

        public void EnableScreen()
        {
            blackScreen.DOFade(1, 0.2f);
            monster.SetActive(true);
        }

    }

}