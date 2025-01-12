using AudioSpace;
using EJETAGame;
using GlobalSpace;
using System.Collections;
using UnityEngine;

namespace ObjectSpace
{
    public class Door : MonoBehaviour, IInteractable
    {
        [Header("Door Settings")]
        [SerializeField] private float rotationDuration;
        public bool canOpen;
        private bool isOpen;
        private float targetRotation = 90f;
        private bool isRotating;
        [SerializeField] private AudioClip audioClip;
        [SerializeField] private bool playAudio;

        public void Interact()
        {
            if (Input.GetKeyDown(GlobalConstants.OPEN_DOOR_KEY) && !isOpen && canOpen)
            {
                StartCoroutine(OpenDoor());
                InteractionText.instance.SetText("");
                canOpen = false;
                if (playAudio)
                {
                    Audio.Instance.PlayAudio(audioClip);
                }
            }
        }

        public void OnInteractEnter()
        {
            if (canOpen)
            {
                InteractionText.instance.SetText("Open Door");
            }
        }

        public void OnInteractExit()
        {
            InteractionText.instance.SetText("");
        }

        //Open Door Method;
        private IEnumerator OpenDoor()
        {
            isOpen = true;
            if (!isRotating)
            {
                
                isRotating = true;
                float startRotation = transform.rotation.eulerAngles.y;
                float timeElapsed = 0f;

                while (timeElapsed < rotationDuration)
                {
                    float newRotation = Mathf.Lerp(startRotation, startRotation + targetRotation, timeElapsed / rotationDuration);
                    transform.rotation = Quaternion.Euler(0, newRotation, 0);
                    timeElapsed += Time.deltaTime;
                    yield return null;
                }

                // Ensure the door is exactly at the target rotation at the end
                transform.rotation = Quaternion.Euler(0, startRotation + targetRotation, 0);
                isRotating = false;
            }
        }
    }

}