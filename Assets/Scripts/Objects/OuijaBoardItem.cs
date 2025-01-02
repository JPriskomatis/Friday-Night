using EJETAGame;
using GlobalSpace;
using PlayerSpace;
using System.Collections;
using UnityEngine;
using VoiceSpace;

namespace ObjectSpace
{
    public class OuijaBoardItem : InteractableItem
    {
        [Header("Extra Components")]
        [SerializeField] private Component voiceRecScript;


        [Header("Move to Position Settings")]
        [SerializeField] Transform targetTransform;
        [SerializeField] float speed;
 
        protected override void BeginInteraction()
        {
            //MovePlayer;
            PlayerMovement.Instance.MoveToPosition(targetTransform, speed);

            //We do this to enable/disable the script of voice recognition;
            ((MonoBehaviour)voiceRecScript).enabled = true;
            InteractionText.instance.SetText("");
            
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                PlayerMovement.Instance.ResetMovement();
                ((MonoBehaviour)voiceRecScript).enabled = false;
                canInteractWith = true;
            } 
        }
    }

}