using GlobalSpace;
using UnityEngine;
using UnityEngine.Events;

namespace ObjectSpace
{
    public class Key : InteractableItem
    {
        [Header("Door key settings")]
        [SerializeField] private Door doorToUnlock;
        [SerializeField] private string keyText;
        [SerializeField] private bool hasEvent;
        public UnityEvent OnKeyPickUp;

        protected override void BeginInteraction()
        {
            //We can open the locked door now;
            PlayerThoughts.Instance.SetText(keyText);
            if(doorToUnlock != null )
            {
                doorToUnlock.canOpen = true;
            }
            if (hasEvent)
            {
                OnKeyPickUp?.Invoke();
            }


            Destroy(gameObject);
        }

    }

}