using System.Collections;
using AudioSpace;
using DG.Tweening;
using EJETAGame;
using GlobalSpace;
using TMPro;
using UISpace;
using UnityEngine;
using UnityEngine.UI;


namespace ObjectSpace
{
    public class TyperWriter : InteractableItem
    {
        [Header("Word to guess")]
        [SerializeField] private string correctWord;
        private string guessWord;

        [Header("Extra components")]
        [SerializeField] private GameObject inputFieldUI;
        [SerializeField] private Animator anim;
        [SerializeField] private AudioClip clip, clip2;
        [SerializeField] private string hintMsg;
        [SerializeField] CanvasGroup keyboardUI;
        private TMP_InputField inputField;

        private bool pressedX;
        private void Awake()
        {
            inputField = inputFieldUI.GetComponent<TMP_InputField>();
        }
        protected override void BeginInteraction()
        {
            InteractionText.instance.SetText("");
            pressedX = false;

            HintMessage.Instance.SetMessage(hintMsg);

            //Pause the game
            PlayerController.Instance.DisableCameraMovement();
            PlayerController.Instance.StopMovement();

            //Enable UI;
            inputFieldUI.SetActive(true);
            inputField.ActivateInputField();
            //            inputField.SetActive(true);

            keyboardUI.DOFade(1, 1f);
            StartCoroutine(CheckForButton());

        }

        public void PlayAudio()
        {
            Audio.Instance.PlayAudio(clip);
        }
        public void PlayAudio2()
        {
            Audio.Instance.PlayAudio(clip2);
        }

        IEnumerator CheckForButton()
        {
            while (!pressedX)
            {
                // Check if the X button is pressed
                if (Input.GetKeyDown(GlobalConstants.ESCAPE_ACTION))
                {
                    Debug.Log("X button pressed!");
                    pressedX = true;
                    
                    EscapeItem();
                }

                // Wait for a frame before checking again
                yield return null;
            }
        }
        

        //Check input;
        public void CheckWord()
        {
            if (guessWord.Equals(correctWord))
            {
                Debug.Log("Correct word");
                Correct();
                

            } else
            {
                Debug.Log("Not correct");
                inputField.ActivateInputField();
            }
        }
        private void Correct()
        {
            keyboardUI.DOFade(0, 1f);

            canInteractWith = false;
            inputFieldUI.SetActive(false);
            anim.SetTrigger("type");
            StartCoroutine(DelayForInputs());
        }

        IEnumerator DelayForInputs()
        {
            HintMessage.Instance.RemoveMessage();
            yield return new WaitForSeconds(6f);
            PlayerController.Instance.EnableCaneraMovement();
            PlayerController.Instance.ResetMovement();

            anim.SetTrigger("reveal");
            this.GetComponent<SphereCollider>().enabled = false;

        }
        //ClearInput;
        public void ClearText()
        {
            guessWord = "";
        }

        public void ReadStringInput()
        {
            guessWord = inputField.text;
            Debug.Log(guessWord);

            CheckWord();

        }

        private void EscapeItem()
        {
            keyboardUI.DOFade(0, 1f);
            HintMessage.Instance.RemoveMessage();
            PlayerController.Instance.EnableCaneraMovement();
            PlayerController.Instance.ResetMovement();


            inputFieldUI.gameObject.SetActive(false);

            canInteractWith = true;
        }
    }

}