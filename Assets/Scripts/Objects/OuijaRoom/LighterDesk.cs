using System;
using System.Collections;
using ObjectSpace;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class LighterDesk : InteractableItem
{
    public static event Action OnGrabLighter;
    [SerializeField] private string lighterTxt;
    [SerializeField] private GameObject lighter;

    public float targetX = 0.8406f;
    public float duration = 0.5f;

    private bool openDrawer;
    protected override void BeginInteraction()
    {
        if (!openDrawer)
        {
            StartCoroutine(MoveByX());
        }
        else
        {
            Debug.Log("grab lighter");
            OnGrabLighter?.Invoke();
            Destroy(lighter);
            Destroy(this);
        }
    }
    private IEnumerator MoveByX()
    {
        openDrawer = true;

        Vector3 startPos = transform.localPosition;
        Vector3 targetPos = new Vector3(targetX, startPos.y, startPos.z);
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.localPosition = Vector3.Lerp(startPos, targetPos, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = targetPos; // Ensure final position is set

        canInteractWith = true;
        interactionText = lighterTxt;
    }
}
