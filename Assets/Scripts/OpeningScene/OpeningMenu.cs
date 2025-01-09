using System;
using System.Collections;
using UnityEngine;

namespace OpeningScene
{
    public class OpeningMenu : MonoBehaviour
    {
        [SerializeField] private Animator cameraAnim, sceneCameraAnim;

        [Header("Game Panels to Activate")]
        [SerializeField] private GameObject menuObject;



        [SerializeField] private AudioSource audioSource;

        public static event Action OnClick;
        private void OnMouseDown()
        {
            Debug.Log($"{gameObject.name} clicked!");
            cameraAnim.SetTrigger("Zoom");
            sceneCameraAnim.SetTrigger("Open");
            StartCoroutine(StartGamePanel());
            OnClick?.Invoke();
        }

        IEnumerator StartGamePanel()
        {
            yield return new WaitForSeconds(2.5f);
            menuObject.SetActive(true);
        }



        public void ButtonClickAudio()
        {
            audioSource.Play();
        }

    }

}