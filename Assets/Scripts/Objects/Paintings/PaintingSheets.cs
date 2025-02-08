using System.Collections;
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
        [SerializeField] private string firstPaintingText;

        private bool canInteract = true;

        private bool hasSheet = true;

        private static bool firstToCheck = true;

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
                StartCoroutine(FirstPainting());
                hasSheet = false;
                anim.SetTrigger("drop");
                canInteract = false;
                InteractionText.instance.SetText("");
                //Destroy(this, 2f);
            }
        }

        IEnumerator FirstPainting()
        {
            if (firstToCheck)
            {
                yield return new WaitForSeconds(1f);
                PlayerThoughts.Instance.SetText(firstPaintingText);
                firstToCheck = false;
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
            this.transform.LookAt(transform);
            canInteract = false;
            InteractionText.instance.SetText("");
            if (hasSheet)
            {
                sheet.gameObject.SetActive(false);
            }
            paintingRenderer.material = newImage;

            
        }
    }

}