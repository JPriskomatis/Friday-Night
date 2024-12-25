using EJETAGame;
using GlobalSpace;
using System.Collections;
using UnityEngine;

namespace ObjectSpace
{
    public class DrawingTable : InteractableItem
    {
        [SerializeField] private Animator anim;

 

        IEnumerator ShowTextRoutine()
        {
            yield return new WaitForSeconds(0.5f);
            PlayerThoughts.Instance.showTextDuration = 3f;
            PlayerThoughts.Instance.SetText("A key? And... a syringe? Why would these be here?");
        }

        protected override void BeginInteraction()
        {
            anim.SetTrigger("Open");
            StartCoroutine(ShowTextRoutine());
        }
    }

}