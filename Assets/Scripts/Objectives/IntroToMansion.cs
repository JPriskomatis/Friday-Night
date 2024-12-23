using GlobalSpace;
using ObjectiveSpace;
using System;
using System.Collections;
using UnityEngine;

public class IntroToMansion : MonoBehaviour
{
    [Header("Intro Settings")]
    [SerializeField] private string[] introText;
    [SerializeField] private string objectiveText;
    [SerializeField] private GameObject blackScreen;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject vhsAudio;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(2f);
        PlayerThoughts.Instance.showTextDuration = 5f;
        for(int i = 0; i < introText.Length; i++)
        {
            PlayerThoughts.Instance.SetText(introText[i]);
            yield return new WaitForSeconds(PlayerThoughts.Instance.showTextDuration);
            
            if (i == 0)
            {
                audioSource.Play();
                yield return new WaitForSeconds(0.5f);

                vhsAudio.SetActive(true);
                blackScreen.SetActive(false);
            }
        }

        ObjectiveManager.Instance.ShowObjective(objectiveText);
        
    }
}
