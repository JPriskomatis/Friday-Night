using ObjectSpace;
using System.Collections;
using UnityEngine;

public class SlideThrough : InteractableItem
{
    [SerializeField] Animator anim;
    [SerializeField] string animationName;
    [SerializeField] float delayRootMotion;
    protected override void BeginInteraction()
    {
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
    }

    private void DisableRootMotion()
    {
        
        anim.applyRootMotion = false;
        StartCoroutine(EnableRootMotion());
    }
}
