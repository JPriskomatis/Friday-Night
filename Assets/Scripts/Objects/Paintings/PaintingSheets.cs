using EJETAGame;
using GlobalSpace;
using UnityEngine;

namespace ObjectSpace
{

    public class PaintingSheets : MonoBehaviour, IInteractable
    {
        [SerializeField] private Animator anim;
        public void Interact()
        {
            if (Input.GetKeyDown(GlobalConstants.INTERACTION))
            {
                anim.SetTrigger("drop");
                Destroy(this, 2f);
            }
        }

        public void OnInteractEnter()
        {
            InteractionText.instance.SetText("Uncover Sheet");
        }

        public void OnInteractExit()
        {
            InteractionText.instance.SetText("");
        }
    }

}