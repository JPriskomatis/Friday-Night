using System.Collections.Generic;
using UnityEngine;

namespace NoteSpace
{
    public class NoteSystem : MonoBehaviour
    {
        [Header("Quad that displays the Note")]
        [SerializeField] private GameObject quad;
        [SerializeField] private Material mat;

        //Available Notes;
        private List<Note> notes = new List<Note>();

        private void OnEnable()
        {
            Note.OnNoteTaken += AcquiredNote;
        }

        private void OnDisable()
        {
            Note.OnNoteTaken -= AcquiredNote;
        }

        //TESTING PURPOSES
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                DisplayNote();
            }
        }

        //Acquire Note;
        public void AcquiredNote(Note note)
        {
            notes.Add(note);
        }

        private void DisplayNote()
        {
            foreach (var note in notes)
            {
                Debug.Log(note.noteSO.title);
                quad.GetComponent<MeshRenderer>().material = note.noteSO.mat;
                Debug.Log(note.noteSO.mat);
            }
        }
    }

}