using System;
using UnityEngine;

namespace ObjectSpace
{
    public class Sledgehammer : InteractableItem
    {
        public static event Action OnTakeHammer;
        protected override void BeginInteraction()
        {

            OnTakeHammer?.Invoke();
            //Audio of interaction;

            //At the end destroy the object;
            Destroy(gameObject);
        }
    }

}