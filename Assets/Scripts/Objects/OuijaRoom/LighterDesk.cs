using System;
using System.Collections;
using AudioSpace;
using ObjectSpace;
using UnityEngine;

public class LighterDesk : InteractableItem
{
    public static event Action OnGrabLighter;
    [SerializeField] private string lighterTxt;
    [SerializeField] private GameObject lighter;
    [SerializeField] private AudioClip ligherClip;

    public static bool lighterAcquired;

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
            Debug.Log($"Subscribers before invoking: {OnGrabLighter?.GetInvocationList().Length ?? 0}");
            lighterAcquired = true;
            Audio.Instance.PlayAudio(ligherClip);
            Destroy(lighter, 0.2f);
            Destroy(this, 0.2f);
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
