using Codice.CM.Common;
using DG.Tweening;
using ObjectSpace;
using System.Collections;
using UnityEngine;

public class SlideThrough : InteractableItem
{
    [SerializeField] Animator anim;
    [SerializeField] string animationName;
    [SerializeField] float delayRootMotion;
    [SerializeField] Transform animPosition;

    private void Awake()
    {
        anim = PlayerController.Instance.gameObject.GetComponent<Animator>();
    }
    protected override void BeginInteraction()
    {
        //Move character to position;
        MoveAndTriggerWhenComplete(animPosition.position, 4f, animationName);

        
    }

    public void MoveAndTriggerWhenComplete(Vector3 targetPositionVector, float speed, string animationName)
    {
        PlayerController.Instance.MoveToPosition(animPosition, speed);
        StartCoroutine(WaitForMovementToComplete(targetPositionVector, animationName));
    }

    private IEnumerator WaitForMovementToComplete(Vector3 animPosition, string animationName)
    {
        float threshold = 0.3f;
        while (Vector3.Distance(PlayerController.Instance.transform.position, animPosition) > threshold)
        {
            yield return null;  // Wait until the next frame
        }

        // Actions after reaching the position
        DisableRootMotion();
        anim.SetTrigger(animationName);
        PlayerController.Instance.GetCameraPos();
    }

    IEnumerator EnableRootMotion()
    {
        yield return new WaitForSeconds(delayRootMotion);
        anim.applyRootMotion = true;
        canInteractWith = true;
        PlayerController.Instance.ResetCameraPos();
        PlayerController.Instance.ResetMovement();
    }

    private void DisableRootMotion()
    {
        
        anim.applyRootMotion = false;
        StartCoroutine(EnableRootMotion());
    }
}
