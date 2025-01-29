using System.Collections.Generic;
using GlobalSpace;
using TMPro;
using UnityEngine;

namespace NoteSpace
{
    public class NoteUI : MonoBehaviour
    {
        [Header("Quad that displays the Note")]
        [SerializeField] private TextMeshProUGUI descriptionLeft;
        [SerializeField] private TextMeshProUGUI descriptionRight;
        [SerializeField] private GameObject noteCanva;
        //We listen for events from out NoteSystem class about new page added to the journal;
        private void OnEnable()
        {
            NoteSystem.OnNotesUpdated += UpdateNoteUI;
        }

        private void OnDisable()
        {
            NoteSystem.OnNotesUpdated -= UpdateNoteUI;
        }

        private void Update()
        {
            if (Input.GetKeyDown(GlobalConstants.NOTE))
            {
                noteCanva.SetActive(!noteCanva.activeSelf);
            }
        }
        private void UpdateNoteUI(List<Note> notes, int index)
        {
            if (notes.Count == 0) return;

            descriptionLeft.text = notes[index].noteSO.description;

            descriptionRight.text = (index + 1 < notes.Count) ? notes[index + 1].noteSO.description : "";
        }

    }
}
