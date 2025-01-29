using System;
using System.Collections.Generic;
using UnityEngine;

namespace NoteSpace
{
    public class NoteSystem : MonoBehaviour
    {
        public static event Action<List<Note>, int> OnNotesUpdated; // Event to update UI

        private List<Note> notes = new List<Note>();
        private int index = 0;

        private void OnEnable()
        {
            Note.OnNoteTaken += AcquiredNote;
        }

        private void OnDisable()
        {
            Note.OnNoteTaken -= AcquiredNote;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                LeftButton();
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                RightButton();
            }
        }

        public void AcquiredNote(Note note)
        {
            notes.Add(note);

            if (notes.Count == 1)
            {
                index = 0;
            }
            else if (notes.Count % 2 == 1)
            {
                index = notes.Count - 1;
            }

            OnNotesUpdated?.Invoke(notes, index);
        }

        public void LeftButton()
        {
            if (index - 2 >= 0)
            {
                index -= 2;
            }
            else
            {
                index = 0;
            }
            OnNotesUpdated?.Invoke(notes, index);
        }

        public void RightButton()
        {
            if (index + 2 < notes.Count)
            {
                index += 2;
            }
            else
            {
                index = (notes.Count % 2 == 0) ? notes.Count - 2 : notes.Count - 1;
            }
            OnNotesUpdated?.Invoke(notes, index);
        }
    }
}
