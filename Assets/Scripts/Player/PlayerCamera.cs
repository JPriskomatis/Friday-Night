using GlobalSpace;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

namespace PlayerSpace
{
    public class PlayerCamera : Singleton<PlayerCamera>
    {
        [Header("Camera Settings")]
        [SerializeField] private Camera mainCamera;

        [Header("Glitch Effects")]
        [SerializeField] private GameObject suddenGlitch;


        public void InitiateGlitchEffect()
        {
            StartCoroutine(GlitchEffect());
        }
        IEnumerator GlitchEffect()
        {
            suddenGlitch.SetActive(true);
            yield return new WaitForSeconds(3f);
            suddenGlitch.SetActive(false);
        }

    }

}