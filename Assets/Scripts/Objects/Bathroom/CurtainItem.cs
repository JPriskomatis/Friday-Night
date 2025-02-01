using AudioSpace;
using EJETAGame;
using GlobalSpace;
using UnityEngine;

namespace ObjectSpace
{
    public class CurtainItem : MonoBehaviour, IInteractable
    {
        [SerializeField] Animator anim;
        [SerializeField] AudioClip clip;
        public void Interact()
        {
            if(Input.GetKeyDown(GlobalConstants.INTERACTION))
            {
                anim.SetTrigger("draw");
                Audio.Instance.PlayAudio(clip);
                Destroy(this, 2f);
            }
        }

        public void OnInteractEnter()
        {
            InteractionText.instance.SetText("Draw Curtains");
        }

        public void OnInteractExit()
        {
            InteractionText.instance.SetText("");
        }

        
    }

}