using UnityEngine;
using System.Collections;
using System;

namespace PlayerSpace
{
    public class RedirectDirection : MonoBehaviour
    {
        public static event Action onChangeDirection;
        public static event Action onAllowMovement;

        [Header("Transform Settings")]
        [SerializeField] private GameObject testCase;


        private void Update()
        {
            
            if (Input.GetKeyDown(KeyCode.G))
            {
                StartCoroutine(ChangePlayerDirection());
            }

        }
        IEnumerator ChangePlayerDirection()
        {
            onChangeDirection?.Invoke();
            yield return new WaitForSeconds(1f);
            transform.LookAt(testCase.transform);
            yield return new WaitForSeconds(2f);
            onAllowMovement?.Invoke();

        }

    }
}
