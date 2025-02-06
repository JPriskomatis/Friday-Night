using System;
using ObjectSpace;
using TriggerSpace;
using UnityEngine;

namespace TriggerSpace
{
    public class PaintingsTrigger : FloorTrigger
    {

        public static event Action OnSetCollider;
        [SerializeField] Animator anim;

        private void OnEnable()
        {
            FinalPainting.OnFlicker += CanInteract;
        }

        private void OnDisable()
        {
            FinalPainting.OnFlicker -= CanInteract;
        }

        private void Start()
        {
            interactable = false;
            interactAgain = false;
        }

        private void CanInteract()
        {
            interactable = true;
        }
        protected override void InitiateAction()
        {
            anim.SetTrigger("boom");
            Debug.Log("Boom");
            OnSetCollider?.Invoke();
        }
    }

}