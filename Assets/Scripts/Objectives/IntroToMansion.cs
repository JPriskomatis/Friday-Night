using GlobalSpace;
using ObjectiveSpace;
using System;
using System.Collections;
using UnityEngine;

public class IntroToMansion : MonoBehaviour
{
    [Header("Intro Text Components")]
    [SerializeField] private string[] introText;
    [SerializeField] private string objectiveText;

    [Header("Audio Settings")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip entranceAudio, cameraAudio;
    [SerializeField] private GameObject vhsAudio;

    [Header("Extra Components")]
    [SerializeField] private GameObject blackScreen;
    [HideInInspector] public bool skipIntro;
    IEnumerator Start()
    {
        if (!skipIntro)
        {
            blackScreen.SetActive(true);
            vhsAudio.SetActive(false);


            yield return new WaitForSeconds(3f);



            //Entrance to Mansion audio;
            

            PlayerThoughts.Instance.showTextDuration = 5f;
            for (int i = 0; i < introText.Length; i++)
            {
                PlayerThoughts.Instance.SetText(introText[i]);
                yield return new WaitForSeconds(PlayerThoughts.Instance.showTextDuration);

            }

            EntranceDoorAudio();
            yield return new WaitForSeconds(5f);

            //Once the monologue ends, we showcase the objective to the player;
            ObjectiveManager.Instance.ShowObjective(objectiveText);


            //Turning the camera on audio;
            audioSource.clip = cameraAudio;
            audioSource.Play();

            yield return new WaitForSeconds(0.5f);

            //Our camera is on so we enable the ambient audio;
            vhsAudio.SetActive(true);
            blackScreen.GetComponent<CanvasGroup>().alpha = 0;
        }
        else
        {
            blackScreen.GetComponent<CanvasGroup>().alpha = 0;
        }
        
        
    }

    private void EntranceDoorAudio()
    {
        audioSource.clip = entranceAudio;
        audioSource.Play();
    }
}
