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
        [SerializeField] private String readNoteHint = "Press J to Read Notes ";

        [SerializeField] PlayerController playerController;

        private bool firstNote;
        private Coroutine hideInstantNoteRoutine;

        //We need a reference of the current note that was picked up
        //in order to activate it OnPickUp or OnPickDown events;
        private Note currentNote;
        private bool voiceNote;

        public GameEvent OpenJournalHint;
        public GameEvent CloseJournalHint;

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
                if (noteCanva.activeSelf)
                {
                    OpenJournalHint.Raise();
                    PlayerController.Instance.StopMovement();
                }
                else
                {
                    CloseJournalHint.Raise();
                    PlayerController.Instance.ResetMovement();
                }
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
            //we store a reference to the current note;
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
            //We wait until the player presses the escape button;
            yield return new WaitUntil(() => Input.GetKeyDown(GlobalConstants.ESCAPE_ACTION));
            currentNote.OnPickdown?.Invoke();
            Destroy(currentNote.gameObject);


            instantNote.SetActive(false);
            HintMessage.Instance.RemoveMessage();
            playerController.EnableCaneraMovement();

            if (firstNote)
            {
                firstNote = false;
                PlayerThoughts.Instance.SetText(readNoteHint);
            }
        }

    }
}
