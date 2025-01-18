using UnityEngine;

namespace TriggerSpace
{
    public abstract class FloorTrigger : MonoBehaviour
    {
        [SerializeField] protected bool interactAgain;
        [SerializeField] protected bool interactable;

        private void Start()
        {
            interactAgain = true;
            interactable = true;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (interactable)
            {
                if (other.CompareTag("Player"))
                {
                    InitiateAction();
                    if (!interactAgain)
                    {
                        interactable = false;
                    }
                }
            }
        }

        protected abstract void InitiateAction();
    }

}