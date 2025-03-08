using TriggerSpace;
using UnityEngine;

public class AudioTrigger : FloorTrigger
{
    [SerializeField] private AudioSource source;
    protected override void InitiateAction()
    {
        source.Play();
        interactable = false;
        Destroy(this, 3f);
    }
}
