using UnityEngine;

namespace OpeningScene
{
    public class OpeningMenu : MonoBehaviour
    {
        [SerializeField] private Animator cameraAnim, sceneCameraAnim;

        private void OnMouseDown()
        {
            Debug.Log($"{gameObject.name} clicked!");
            cameraAnim.SetTrigger("Zoom");
            sceneCameraAnim.SetTrigger("Open");
        }


    }

}