using System.Collections;
using GlobalSpace;
using TMPro;
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
        private TMP_InputField inputField;

        private bool pressedX;
        private void Awake()
        {
            inputField = inputFieldUI.GetComponent<TMP_InputField>();
        }
        protected override void BeginInteraction()
        {
            pressedX = false;
            //Pause the game
            PlayerController.Instance.DisableCameraMovement();
            PlayerController.Instance.StopMovement();

            //Enable UI;
            inputFieldUI.SetActive(true);
            inputField.ActivateInputField();
            //            inputField.SetActive(true);

            StartCoroutine(CheckForButton());
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
            canInteractWith = false;
            inputFieldUI.SetActive(false);
            anim.SetTrigger("type");
            StartCoroutine(DelayForInputs());
        }

        IEnumerator DelayForInputs()
        {
            yield return new WaitForSeconds(6f);
            PlayerController.Instance.EnableCaneraMovement();
            PlayerController.Instance.ResetMovement();

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
            PlayerController.Instance.EnableCaneraMovement();
            PlayerController.Instance.ResetMovement();


            inputFieldUI.gameObject.SetActive(false);

            canInteractWith = true;
        }
    }

}