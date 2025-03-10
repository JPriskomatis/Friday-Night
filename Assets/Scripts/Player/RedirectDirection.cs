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
        [SerializeField] private Transform targetToLook;
        [SerializeField] private GameObject glitchEffectGO;
        [SerializeField] private GameObject mainCamera;


        [SerializeField] private TargetToFollow targetGameObject;

        //private void Update()
        //{
        //    if (Input.GetKeyDown(KeyCode.G))
        //    {
        //        StartCoroutine(ChangePlayerDirection());
        //    }
        //}

        public void GetTargetToFollow()
        {
            if (targetGameObject.GetTarget() != null)
            {
                StartCoroutine(ChangePlayerDirection(targetGameObject.GetTarget().transform));
            }
        }
        IEnumerator ChangePlayerDirection(Transform target)
        {
            PlayerController.Instance.StopMovement();
            // Announce to our movement script that we no longer can control the camera
            onChangeDirection?.Invoke();

            // Enable the CameraGlitchEffect
            //glitchEffectGO.SetActive(true);

            // Rotate the Player horizontally
            Quaternion startPlayerRotation = transform.rotation;
            Vector3 targetDirection = target.position - transform.position;
            targetDirection.y = 0; // Keep only the horizontal direction
            Quaternion targetPlayerRotation = Quaternion.LookRotation(targetDirection);

            float elapsedTime = 0f;
            while (elapsedTime < 0.5f)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / 0.5f;

                transform.rotation = Quaternion.Slerp(startPlayerRotation, targetPlayerRotation, t);

                yield return null;
            }
            transform.rotation = targetPlayerRotation;

            // Rotate the Camera vertically to look at the target
            Quaternion startCameraRotation = mainCamera.transform.rotation;
            Vector3 cameraToTarget = target.position - mainCamera.transform.position;
            Quaternion targetCameraRotation = Quaternion.LookRotation(cameraToTarget);

            elapsedTime = 0f;
            while (elapsedTime < 0.5f) // Adjust camera rotation duration as needed
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / 0.5f;

                mainCamera.transform.rotation = Quaternion.Slerp(startCameraRotation, targetCameraRotation, t);

                yield return null;
            }
            mainCamera.transform.rotation = targetCameraRotation;

            // Restablish movement of the camera
            onAllowMovement?.Invoke();
            PlayerController.Instance.ResetMovement();

        }
    }
}
