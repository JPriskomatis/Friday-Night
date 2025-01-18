using System.Collections;
using UnityEngine;

namespace ObjectSpace
{
    public class HideUnderTable : Hide
    {
        
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private GameObject monster;


        protected override void BeginInteraction()
        {
            base.BeginInteraction();
            audioSource.Play();
            isHiding = true;
        }
        protected override void EndAnimation()
        {
            base.EndAnimation();
            isHiding = false;
        }
    }

}