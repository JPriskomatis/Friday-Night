using System.Collections;
using UnityEngine;

namespace ObjectSpace
{
    public class HideUnderTable : Hide
    {
        
        [SerializeField] private GameObject monster;


        protected override void BeginInteraction()
        {
            base.BeginInteraction();
            isHiding = true;
        }
        protected override void EndAnimation()
        {
            base.EndAnimation();
            StartCoroutine(DelayFoHide());
        }

        IEnumerator DelayFoHide()
        {
            yield return new WaitForSeconds(1f);
            isHiding = false;

        }
    }

}