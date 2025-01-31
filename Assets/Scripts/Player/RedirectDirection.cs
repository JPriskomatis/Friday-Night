using UnityEngine;
using System.Collections;
using System;

namespace PlayerSpace
{
    public class RedirectDirection : MonoBehaviour
    {
        public static event Action onChangeDirection;
        public static event Action onAllowMovement;

        [Header("Components Settings")]
        [SerializeField] private Transform testCase;
        [SerializeField] private GameObject glitchEffectGO;
        [SerializeField] private GameObject mainCamera;

        //private void Update()
        //{
        //    if (Input.GetKeyDown(KeyCode.G))
        //    {
        //        StartCoroutine(ChangePlayerDirection());
        //    }
        //}

        IEnumerator ChangePlayerDirection()
        {
            // Announce to our movement script that we no longer can control the camera
            onChangeDirection?.Invoke();

            // Enable the CameraGlitchEffect
            glitchEffectGO.SetActive(true);

            // Rotate the Player horizontally
            Quaternion startPlayerRotation = transform.rotation;
            Vector3 targetDirection = testCase.position - transform.position;
            targetDirection.y = 0; // Keep only the horizontal direction
            Quaternion targetPlayerRotation = Quaternion.LookRotation(targetDirection);

            float elapsedTime = 0f;
            while (elapsedTime < 2f)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / 2f;

                transform.rotation = Quaternion.Slerp(startPlayerRotation, targetPlayerRotation, t);

                yield return null;
            }
            transform.rotation = targetPlayerRotation;

            // Rotate the Camera vertically to look at the target
            Quaternion startCameraRotation = mainCamera.transform.rotation;
            Vector3 cameraToTarget = testCase.position - mainCamera.transform.position;
            Quaternion targetCameraRotation = Quaternion.LookRotation(cameraToTarget);

            elapsedTime = 0f;
            while (elapsedTime < 1f) // Adjust camera rotation duration as needed
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / 1f;

                mainCamera.transform.rotation = Quaternion.Slerp(startCameraRotation, targetCameraRotation, t);

                yield return null;
            }
            mainCamera.transform.rotation = targetCameraRotation;

            // Restablish movement of the camera
            onAllowMovement?.Invoke();
            // Remove the CameraGlitchEffect
            glitchEffectGO.SetActive(false);
        }
    }
}
