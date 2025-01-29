using System.Collections;
using System.Collections.Generic;
using GlobalSpace;
using TMPro;
using UISpace;
using UnityEngine;
using UnityEngine.UI;

namespace NoteSpace
{
    public class NoteUI : MonoBehaviour
    {
        [Header("Notebook Pages")]
        [SerializeField] private TextMeshProUGUI descriptionLeft;
        [SerializeField] private TextMeshProUGUI descriptionRight;
        [SerializeField] private GameObject noteCanva;

        [Header("Instant Note UI")]
        [SerializeField] private GameObject instantNote;
        [SerializeField] private Image noteMat;


        private Coroutine hideInstantNoteRoutine;

        //We listen for events from out NoteSystem class about new page added to the journal;
        private void OnEnable()
        {
            NoteSystem.OnNotesUpdated += UpdateNoteUI;
            NoteSystem.OnNotePickup += ShowcaseNote;
        }

        private void OnDisable()
        {
            NoteSystem.OnNotesUpdated -= UpdateNoteUI;
            NoteSystem.OnNotePickup -= ShowcaseNote;
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

        private void ShowcaseNote(Note note)
        {
            instantNote.SetActive(true);
            noteMat.material = note.noteSO.material;

            EnableHint();

            if (hideInstantNoteRoutine != null)
            {
                StopCoroutine(hideInstantNoteRoutine);
            }
            hideInstantNoteRoutine = StartCoroutine(WaitForGKey());
        }

        private void EnableHint()
        {
            VoiceRecUI.Instance.SetMessage("Press X to escape");
        }
        private IEnumerator WaitForGKey()
        {
            // Wait until the user presses "G"
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.X));

            instantNote.SetActive(false);
            VoiceRecUI.Instance.RemoveMessage();
        }

    }
}
