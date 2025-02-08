using EJETAGame;
using GlobalSpace;
using PlayerSpace;
using System.Collections;
using UISpace;
using UnityEngine;
using VoiceSpace;

namespace ObjectSpace
{
    public class OuijaBoardItem : InteractableItem
    {
        [Header("Extra Components")]
        [SerializeField] private Component voiceRecScript;
        [SerializeField] private string hintMessage;
        [SerializeField] private string findLighterTxt;


        [Header("Move to Position Settings")]
        [SerializeField] Transform targetTransform;
        [SerializeField] float speed;

        private bool hasLighter;

        private void OnEnable()
        {
            LighterDesk.OnGrabLighter += HasLighter;
        }

        private void OnDisable()
        {
            LighterDesk.OnGrabLighter -= HasLighter;
        }
        private void HasLighter()
        {
            hasLighter = true;
        }
        protected override void BeginInteraction()
        {
            if (hasLighter)
            {
                //MovePlayer;
                PlayerController.Instance.MoveToPosition(targetTransform, speed);

                //We do this to enable/disable the script of voice recognition;
                ((MonoBehaviour)voiceRecScript).enabled = true;
                InteractionText.instance.SetText("");
                HintMessage.Instance.SetMessage(hintMessage);
            }
            else
            {
                PlayerThoughts.Instance.SetText(findLighterTxt);
                canInteractWith = true;
            }
            
            
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                PlayerController.Instance.ResetMovement();
                ((MonoBehaviour)voiceRecScript).enabled = false;
                canInteractWith = true;
                HintMessage.Instance.RemoveMessage();
            } 
        }
    }

}