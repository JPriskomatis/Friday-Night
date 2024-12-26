using GlobalSpace;
using UnityEngine;

namespace ObjectSpace
{
    public class Key : InteractableItem
    {
        [Header("Door key settings")]
        [SerializeField] private Door doorToUnlock;
        [SerializeField] private SphereCollider sphereCollider;
        [SerializeField] private string keyText;

        private void OnEnable()
        {
            DrawingTable.OnDraw += EnableCollision;
        }

        private void OnDisable()
        {
            DrawingTable.OnDraw -= EnableCollision;
        }

        private void EnableCollision()
        {
            sphereCollider.enabled = true;
        }
        protected override void BeginInteraction()
        {
            //We can open the locked door now;
            PlayerThoughts.Instance.SetText(keyText);
            doorToUnlock.canOpen = true;
            Destroy(gameObject);
        }
    }

}