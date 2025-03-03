using System;
using AudioSpace;
using ObjectSpace;
using UnityEngine;
using UnityEngine.Events;

namespace NoteSpace
{
    public class Note : InteractableItem
    {
        public NoteSO noteSO;

        public static event Action<Note> OnNoteTaken;
        [SerializeField] private bool hasEvent;
        [SerializeField] private AudioClip clip;
        public UnityEvent OnPickup;
        public UnityEvent OnPickdown;


        //private void Update()
        //{
        //    if (Input.GetKeyDown(KeyCode.O))
        //    {
        //        OnNoteTaken?.Invoke(this);
        //    }
        //}

        private void OnEnable()
        {
            OnPickup.AddListener(StopCameraRotate);
            OnPickdown.AddListener(ResetCameraRotate);
        }

        private void OnDisable()
        {
            OnPickup.RemoveListener(StopCameraRotate);
            OnPickdown.RemoveListener(ResetCameraRotate);

        }
        protected override void BeginInteraction()
        {
            //Display Note;
            //DisplayNote();
            OnNoteTaken?.Invoke(this);

            if (hasEvent)
            {
                OnPickup?.Invoke();
                Audio.Instance.PlayAudio(clip);
            }
            
            Destroy(gameObject);

            
        }
        //When we pick up a note we want to lock the camera and unlock it when we put the note down;
        public void StopCameraRotate()
        {
            PlayerController.Instance.DisableCameraMovement();
        }

        public void ResetCameraRotate()
        {
            PlayerController.Instance.ResetMovement();
        }
    }

}