using UnityEngine;
using System.Collections;

namespace PlayerSpace
{
    public class RedirectCamera : MonoBehaviour
    {
        [Header("Camera Settings")]
        public MonoBehaviour firstPersonController;
        [Header("Transform Settings")]
        [SerializeField] private GameObject testCase;
        private Vector3 savedCameraPosition;
        private Quaternion savedCameraRotation;

        private bool isCameraRedirected = false; 

        private void Update()
        {
            // Detect when the "G" key is pressed
            if (Input.GetKeyDown(KeyCode.G))
            {
                firstPersonController.enabled = false;

                transform.LookAt(testCase.transform);

                StartCoroutine(ReEnableController());

            }

        }

        IEnumerator ReEnableController()
        {
            yield return new WaitForSeconds(2f);
            firstPersonController.enabled = true;
        }



    }
}
