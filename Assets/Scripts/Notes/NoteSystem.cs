using System.Collections.Generic;
using GlobalSpace;
using TMPro;
using UnityEngine;

namespace NoteSpace
{
    public class NoteSystem : MonoBehaviour
    {
        [Header("Quad that displays the Note")]

        [SerializeField] private TextMeshProUGUI descriptionLeft;
        [SerializeField] private TextMeshProUGUI descriptionRight;

        [SerializeField] private GameObject noteBook;

        private int index;

        private bool isOpen;


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
            if (Input.GetKeyDown(GlobalConstants.NOTE))
            {
                DisplayNote();
            }
        }

        //Acquire Note;
        public void AcquiredNote(Note note)
        {
            notes.Add(note);
            SetNoteUI();
        }

        private void SetNoteUI()
        {
            foreach (Note note in notes)
            {
                if (index % 2 == 0)
                {
                    descriptionLeft.text = notes[index].noteSO.description;
                }
                else
                {
                    descriptionRight.text = notes[index].noteSO.description;
                }
                index++;
            }
        }

        //This function sets up the notes in the notebook;
        private void DisplayNote()
        {
            if (isOpen)
            {
                noteBook.SetActive(false);
            }
            else
            {
                noteBook.SetActive(true);
                
                //TODO:
                //if player stays in journal for took long
                //initiate a jumpscare;
                
            }
            isOpen = !isOpen;

        }
    }

}