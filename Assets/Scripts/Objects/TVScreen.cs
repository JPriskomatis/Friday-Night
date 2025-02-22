using EJETAGame;
using GlobalSpace;
using UISpace;
using UnityEngine;
using UnityEngine.Video;

namespace ObjectSpace
{
    public class TVScreen : InteractableItem
    {
        [SerializeField] private Transform targetPos;
        [SerializeField] private float speed;
        [SerializeField] private string messageHint;
        [Header("Extra Components")]
        [SerializeField] private Component tvMessage;
        [SerializeField] private VideoPlayer VideoPlayer;
        protected override void BeginInteraction()
        {
            PlayerController.Instance.MoveToPosition(targetPos, speed);
            InteractionText.instance.SetText("");

            ((MonoBehaviour)tvMessage).enabled = true;

            //Show the UI Indications;
            HintMessage.Instance.SetMessage(messageHint);

            VideoPlayer.Play();
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