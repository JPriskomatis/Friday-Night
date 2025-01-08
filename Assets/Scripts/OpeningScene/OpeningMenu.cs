using System;
using UnityEngine;

namespace OpeningScene
{
    public class OpeningMenu : MonoBehaviour
    {
        [SerializeField] private Animator cameraAnim, sceneCameraAnim;

        public static event Action OnClick;
        private void OnMouseDown()
        {
            Debug.Log($"{gameObject.name} clicked!");
            cameraAnim.SetTrigger("Zoom");
            sceneCameraAnim.SetTrigger("Open");
            OnClick?.Invoke();
        }


    }

}