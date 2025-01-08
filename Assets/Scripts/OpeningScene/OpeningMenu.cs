using System;
using System.Collections;
using UnityEngine;

namespace OpeningScene
{
    public class OpeningMenu : MonoBehaviour
    {
        [SerializeField] private Animator cameraAnim, sceneCameraAnim;
        [SerializeField] private GameObject menuObject;

        public static event Action OnClick;
        private void OnMouseDown()
        {
            Debug.Log($"{gameObject.name} clicked!");
            cameraAnim.SetTrigger("Zoom");
            sceneCameraAnim.SetTrigger("Open");
            StartCoroutine(ActivateMenu());
            OnClick?.Invoke();
        }

        IEnumerator ActivateMenu()
        {
            yield return new WaitForSeconds(2.5f);
            menuObject.SetActive(true);
        }

    }

}