using EJETAGame;
using PlayerSpace;
using UISpace;
using UnityEditor.VersionControl;
using UnityEngine;

namespace ObjectSpace
{
    public class TVScreen : InteractableItem
    {
        [SerializeField] private Transform targetPos;
        [SerializeField] private float speed;
        [SerializeField] private string messageHint;
        [Header("Extra Components")]
        [SerializeField] private Component tvMessage;
        protected override void BeginInteraction()
        {
            PlayerController.Instance.MoveToPosition(targetPos, speed);
            InteractionText.instance.SetText("");

            ((MonoBehaviour)tvMessage).enabled = true;

            //Show the UI Indications;
            VoiceRecUI.Instance.SetMessage(messageHint);
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                ((MonoBehaviour)tvMessage).enabled = false;
                PlayerController.Instance.ResetMovement();
                VoiceRecUI.Instance.RemoveMessage();
            }
        }
    }

}