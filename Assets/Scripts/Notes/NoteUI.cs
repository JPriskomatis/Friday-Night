using System;
using System.Collections;
using System.Collections.Generic;
using GlobalSpace;
using TMPro;
using UISpace;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NoteSpace
{
    public class NoteUI : MonoBehaviour
    {


        [Header("Notebook Pages")]
        [SerializeField] private TextMeshProUGUI descriptionLeft;
        [SerializeField] private TextMeshProUGUI descriptionRight;
        [SerializeField] private TextMeshProUGUI pageNumber;
        [SerializeField] private GameObject noteCanva;

        [Header("Instant Note UI")]
        [SerializeField] private GameObject instantNote;
        [SerializeField] private Image noteMat;

        [SerializeField] PlayerController playerController;

        private bool firstNote;
        private Coroutine hideInstantNoteRoutine;

        public UnityEvent NoteAction;
        private Note currentNote;
        private bool voiceNote;

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

        private void Start()
        {
            firstNote = true;
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

            //upper round of number;
            pageNumber.text = "Page " + Mathf.CeilToInt((index + 1) / 2f).ToString();
            descriptionLeft.text = notes[index].noteSO.description;

            descriptionRight.text = (index + 1 < notes.Count) ? notes[index + 1].noteSO.description : "";
        }

        private void ShowcaseNote(Note note)
        {
            currentNote = note;

            playerController.DisableCameraMovement();

            instantNote.SetActive(true);
            noteMat.material = note.noteSO.material;

            EnableHint();

            if (hideInstantNoteRoutine != null)
            {
                StopCoroutine(hideInstantNoteRoutine);
            }
            //if (note.CompareTag("Voice"))
            //{
            //    voiceNote = true;
            //}
            //else
            //{
            //    voiceNote = false;
            //}
            hideInstantNoteRoutine = StartCoroutine(WaitForGKey());
        }

        private void EnableHint()
        {
            HintMessage.Instance.SetMessage("Press X to escape");
        }
        private IEnumerator WaitForGKey()
        {
            // Wait until the user presses "G"
            yield return new WaitUntil(() => GlobalConstants.ESCAPE_ACTION);
            currentNote.OnPickdown?.Invoke();
            //if (voiceNote)
            //{
            //    NoteAction?.Invoke();
            //}


            instantNote.SetActive(false);
            HintMessage.Instance.RemoveMessage();
            playerController.EnableCaneraMovement();

            if (firstNote)
            {
                firstNote = false;
                PlayerThoughts.Instance.SetText("Press J to Read Notes ");
            }
        }

    }
}
