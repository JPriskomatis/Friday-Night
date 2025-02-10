using System;
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
        public UnityEvent OnPickup;
        public UnityEvent OnPickdown;


        //private void Update()
        //{
        //    if (Input.GetKeyDown(KeyCode.O))
        //    {
        //        OnNoteTaken?.Invoke(this);
        //    }
        //}
        protected override void BeginInteraction()
        {
            //Display Note;
            //DisplayNote();
            OnNoteTaken?.Invoke(this);

            if (hasEvent)
            {
                OnPickup?.Invoke();
            }
            
            Destroy(gameObject);

            
        }
    }

}