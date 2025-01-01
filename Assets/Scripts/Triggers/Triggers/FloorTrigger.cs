using UnityEngine;

namespace TriggerSpace
{
    public abstract class FloorTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                InitiateAction();
            }
        }

        protected abstract void InitiateAction();
    }

}