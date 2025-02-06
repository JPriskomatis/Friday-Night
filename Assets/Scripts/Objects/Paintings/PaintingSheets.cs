using System.Collections.Generic;
using EJETAGame;
using GlobalSpace;
using UnityEngine;

namespace ObjectSpace
{

    public class PaintingSheets : MonoBehaviour, IInteractable
    {
        [SerializeField] private Animator anim;
        [SerializeField] private MeshRenderer paintingRenderer;

        [SerializeField] private Material newImage;

        private bool canInteract = true;

        private bool hasSheet = true;
        [SerializeField] private GameObject sheet;
        private void OnEnable()
        {
            FinalPainting.OnChangeImages += ChangeMaterial;
        }

        private void OnDisable()
        {
            FinalPainting.OnChangeImages -= ChangeMaterial;
        }
        public void Interact()
        {
            if (Input.GetKeyDown(GlobalConstants.INTERACTION) && canInteract)
            {
                hasSheet = false;
                anim.SetTrigger("drop");
                canInteract = false;
                InteractionText.instance.SetText("");
                //Destroy(this, 2f);
            }
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

        private void ChangeMaterial(Transform transform)
        {
            canInteract = false;
            InteractionText.instance.SetText("");
            if (hasSheet)
            {
                sheet.gameObject.SetActive(false);
            }
            paintingRenderer.material = newImage;

            this.transform.LookAt(transform);
        }
    }

}