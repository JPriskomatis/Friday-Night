using EJETAGame;
using GlobalSpace;
using UnityEngine;

namespace ObjectSpace
{
    public abstract class InteractableItem : MonoBehaviour, IInteractable
    {
        [Header("Interaction Settings")]
        [SerializeField] protected bool canInteractWith;
        [SerializeField] protected string interactionText;

        private void Start()
        {
            canInteractWith = true;
        }
        public void Interact()
        {
            if(Input.GetKeyDown(GlobalConstants.INTERACTION) && canInteractWith)
            {
                BeginInteraction();
                canInteractWith = false;
            }
        }

        public void OnInteractEnter()
        {
            if (canInteractWith)
            {
                InteractionText.instance.SetText(interactionText);
            }
        }

        public void OnInteractExit()
        {
            
        }

        //This is the actual interaction that each item script should create the action that we want
        //when the player interacts with the object (eg. run open door script);
        protected abstract void BeginInteraction();
    }

}