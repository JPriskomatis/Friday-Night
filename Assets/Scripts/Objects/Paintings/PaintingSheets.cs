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
            paintingRenderer.material = newImage;

            this.transform.LookAt(transform);
        }
    }

}