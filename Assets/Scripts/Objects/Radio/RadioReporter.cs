using System.Collections;
using ObjectSpace;
using UnityEngine;

public class RadioReporter : InteractableItem
{
    [SerializeField] private AudioSource audiosource;
    [SerializeField] private AudioClip reporterAudio, staticClip, morseClip;

    protected override void BeginInteraction()
    {
        audiosource.clip = reporterAudio;
        audiosource.Play();
        StartCoroutine(PlayStaticClipWhenFinished());
    }

    private IEnumerator PlayStaticClipWhenFinished()
    {
        yield return new WaitForSeconds(reporterAudio.length);

        audiosource.clip = staticClip;
        audiosource.Play();

        yield return new WaitForSeconds(staticClip.length);

        audiosource.clip = morseClip;
        audiosource.Play();
        audiosource.loop = true;
    }


}
