using EJETAGame;
using PlayerSpace;
using System.Collections;
using UISpace;
using UnityEngine;

namespace ObjectSpace
{
    public class BloodAppear : InteractableItem
    {

        [SerializeField] private Transform targetPos;
        [SerializeField] private float speed;
        [SerializeField] private string messageHint;
        [Header("Extra Components")]
        [SerializeField] private Component voiceRecScript;


        protected override void BeginInteraction()
        {
            //Move player to position;
            PlayerMovement.Instance.MoveToPosition(targetPos, speed);
            InteractionText.instance.SetText("");

            ((MonoBehaviour)voiceRecScript).enabled = true;

            //Show the UI Indications;
            VoiceRecUI.Instance.SetMessage(messageHint);

        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                ((MonoBehaviour)voiceRecScript).enabled = false;
                PlayerMovement.Instance.ResetMovement();
                VoiceRecUI.Instance.RemoveMessage();
            }
        }

    }

}