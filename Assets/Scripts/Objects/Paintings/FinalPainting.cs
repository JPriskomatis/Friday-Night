using System;
using System.Collections;
using EJETAGame;
using GlobalSpace;
using UnityEngine;
using UnityEngine.Events;

namespace ObjectSpace
{
    public class FinalPainting : MonoBehaviour, IInteractable
    {
        public static event Action<Transform> OnChangeImages;
        public static event Action OnFlicker;

        public UnityEvent OnCloseDoor;
        [Header("Components")]
        [SerializeField] private AudioSource source;
        [SerializeField] private Animator anim;
        [SerializeField] private Transform paintingTransform;
        private bool canInteract = true;

        public void Interact()
        {
            if (Input.GetKeyDown(GlobalConstants.INTERACTION) && canInteract)
            {
                anim.SetTrigger("drop");
                canInteract = false;
                InteractionText.instance.SetText("");
                StartCoroutine(ReversePaintings());
            }
        }

        IEnumerator ReversePaintings()
        {
            yield return new WaitForSeconds(1f);
            source.Play();
            OnChangeImages?.Invoke(paintingTransform);
            OnFlicker?.Invoke();
            OnCloseDoor?.Invoke();

        }



        public void OnInteractEnter()
        {
            if (canInteract)
            {
                InteractionText.instance.SetText("Uncover Sheet");
            }
        }

        public void OnInteractExit()
        {
            InteractionText.instance.SetText("");
        }

    }

}
