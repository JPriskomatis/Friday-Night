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
            PlayerController.Instance.MoveToPosition(targetPos, speed);
            InteractionText.instance.SetText("");

            ((MonoBehaviour)voiceRecScript).enabled = true;

            //Show the UI Indications;
            HintMessage.Instance.SetMessage(messageHint);

        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                ((MonoBehaviour)voiceRecScript).enabled = false;
                PlayerController.Instance.ResetMovement();
                HintMessage.Instance.RemoveMessage();
            }
        }

    }

}