using System.Collections;
using ObjectSpace;
using UnityEngine;

public class RadioReporter : InteractableItem
{
    [SerializeField] private AudioSource audiosource;
    [SerializeField] private AudioClip reporterAudio;

    protected override void BeginInteraction()
    {
        audiosource.clip = reporterAudio;
        audiosource.Play();
        StartCoroutine(DelayToInteract());
    }

    private IEnumerator DelayToInteract()
    {
        yield return new WaitForSeconds(16.5f);
        canInteractWith = true;
    }


}
