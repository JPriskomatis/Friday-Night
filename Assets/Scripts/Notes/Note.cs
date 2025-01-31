using System;
using ObjectSpace;
using UnityEngine;

namespace NoteSpace
{
    public class Note : InteractableItem
    {
        public NoteSO noteSO;

        public static event Action<Note> OnNoteTaken;


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
            Destroy(gameObject);
            
        }
    }

}