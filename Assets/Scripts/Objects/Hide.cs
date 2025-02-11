using System.Collections;
using GlobalSpace;
using UISpace;
using UnityEngine;

namespace ObjectSpace
{
    public abstract class Hide : InteractableItem
    {
        [SerializeField] private Animator anim;
        [SerializeField] private SphereCollider sphere;
        [SerializeField] private GameObject micVolume;
        [SerializeField] private GameObject micVolumeUI;
        [SerializeField] private string hintMessageTxt;
        public static bool isHiding;

        [Header("Specific Animation's Settings")]
        [SerializeField] protected int rootMotionDelay;
        [SerializeField] protected string enterAnimationName;
        [SerializeField] protected string exitAnimationName;

        [SerializeField] HintMessage hintMessage;

        private void Awake()
        {
            sphere = GetComponent<SphereCollider>();
        }
        

        protected virtual void EndAnimation()
        {
            anim.SetTrigger(exitAnimationName);
            StartCoroutine(EnableRootMotion());
            micVolume.SetActive(false);
            micVolumeUI.SetActive(false);

            canInteractWith = true;

        }
        protected override void BeginInteraction()
        {
            anim.SetTrigger(enterAnimationName);

            //Enable Mic Detection;
            micVolume.SetActive(true);
            micVolumeUI.SetActive(true);

            sphere.enabled = false;
            anim.applyRootMotion = false;

            //Enable hint message;
            hintMessage.SetMessage(hintMessageTxt);

            StartCoroutine(CheckForInput());
        }

        IEnumerator CheckForInput()
        {
            yield return new WaitUntil(() => Input.GetKeyDown(GlobalConstants.ESCAPE_ACTION));
            hintMessage.RemoveMessage();
            EndAnimation();
        }

        IEnumerator EnableRootMotion()
        {
            yield return new WaitForSeconds(rootMotionDelay);
            anim.applyRootMotion = true;
            sphere.enabled = true;
        }


    }

}