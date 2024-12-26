using System.Collections;
using UnityEngine;

namespace ObjectSpace
{
    public class Hide : InteractableItem
    {
        [SerializeField] private Animator anim;
        [SerializeField] private SphereCollider sphere;

        private void Awake()
        {
            sphere = GetComponent<SphereCollider>();
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                anim.SetTrigger("KitchenOut");
                StartCoroutine(EnableRootMotion());
            }
        }
        protected override void BeginInteraction()
        {
            anim.SetTrigger("Kitchen");
            sphere.enabled = false;
            anim.applyRootMotion = false;
        }
        IEnumerator EnableRootMotion()
        {
            yield return new WaitForSeconds(3f);
            anim.applyRootMotion = true;
            sphere.enabled = true;
        }
    }

}