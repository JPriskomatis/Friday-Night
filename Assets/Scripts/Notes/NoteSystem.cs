using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace NoteSpace
{
    public class NoteSystem : MonoBehaviour
    {
        [Header("Quad that displays the Note")]
        [SerializeField] private GameObject quadImage;
        [SerializeField] private GameObject quadDescription;

        [SerializeField] private GameObject noteBook;

        private int index;


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

        //This function sets up the notes in the notebook;
        private void DisplayNote()
        {
            //We activate the book;
            noteBook.SetActive(true);

            //We pass the information to the UI;
            Debug.Log(notes[index].noteSO.title);
            
            quadImage.GetComponent<MeshRenderer>().material = notes[index].noteSO.matImage;
            quadDescription.GetComponent<MeshRenderer>().material = notes[index].noteSO.matDescription;


            index++;
        }
    }

}