using EJETAGame;
using GlobalSpace;
using UISpace;
using UnityEngine;

namespace ObjectSpace
{
    public abstract class InteractableItem : MonoBehaviour, IInteractable
    {
        [Header("Interaction Settings")]
        [SerializeField] protected bool canInteractWith;
        [SerializeField] protected string interactionText;
        private static bool firstInteract = true;

        private void Start()
        {
            canInteractWith = true;
        }
        public void Interact()
        {
            if(Input.GetKeyDown(GlobalConstants.INTERACTION) && canInteractWith)
            {
                canInteractWith = false;
                BeginInteraction();
            }
        }

        public void OnInteractEnter()
        {
            if (firstInteract)
            {
                PlayerThoughts.Instance.SetText("Press " + GlobalConstants.INTERACTION + " to interact");
                firstInteract = false;
            }
            if (canInteractWith)
            {
                InteractionText.instance.SetText(interactionText);
            }
            else
            {
                InteractionText.instance.SetText("");
            }
        }

        public void OnInteractExit()
        {
            InteractionText.instance.SetText("");
        }

        //This is the actual interaction that each item script should create the action that we want
        //when the player interacts with the object (eg. run open door script);
        protected abstract void BeginInteraction();
    }

}