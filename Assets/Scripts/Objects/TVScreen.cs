using EJETAGame;
using UISpace;
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
            HintMessage.Instance.SetMessage(messageHint);
        }
        private void Update()
        {
            if (Input.GetKeyDown(GlobalConstants.ESCAPE_ACTION))
            {
                ((MonoBehaviour)tvMessage).enabled = false;
                PlayerController.Instance.ResetMovement();
                HintMessage.Instance.RemoveMessage();
            }
        }
    }

}