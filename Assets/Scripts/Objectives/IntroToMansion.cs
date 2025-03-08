using AudioSpace;
using DG.Tweening;
using GlobalSpace;
using ObjectiveSpace;
using System;
using System.Collections;
using TMPro;
using UISpace;
using UnityEngine;
using UnityEngine.Events;

public class IntroToMansion : MonoBehaviour
{
    [Header("Intro Text Components")]
    [SerializeField] private string[] introText;
    [SerializeField] private string objectiveText;
    [SerializeField] private TextMeshProUGUI introTextUI;
    [SerializeField] private float showTextDuration;

    [Header("Audio Settings")]
    [SerializeField] private AudioSource audioSource, thunderSource, footstepsSource;
    [SerializeField] private AudioClip entranceAudio, cameraAudio, rainAudio;
    [SerializeField] private GameObject vhsAudio;

    [Header("Extra Components")]
    [SerializeField] private GameObject blackScreen;
    [HideInInspector] public bool skipIntro;
    [SerializeField] private RecTimer recTimer;
    [SerializeField] private string hintMsg;

    [Header("Typewriter Effect")]
    [SerializeField] private float typingSpeed = 0.05f;
    private bool isTyping = false;
    private bool sentenceCompleted = false;

    public static Action OnIntroFinish;


    public UnityEvent EnablePlayerCamera;

    public GameEvent TypingAudio;
    public GameEvent StopTypingAudio;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isTyping)
            {
                //if still typing, instantly complete sentence;
                isTyping = false;
            }
            else if (sentenceCompleted)
            {
                //if sentence is completed, move to the next immediately;
                sentenceCompleted = false;
            }
        }
    }

    private IEnumerator TypeText(string text)
    {
        introTextUI.text = "";
        isTyping = true;
        sentenceCompleted = false;

        TypingAudio.Raise();
        foreach (char c in text)
        {
            if (!isTyping)
            {
                //we instantly complete the sentence;
                introTextUI.text = text;
                break;
            }

            introTextUI.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
        sentenceCompleted = true;
        StopTypingAudio.Raise();
    }

    private IEnumerator Start()
    {


        footstepsSource.Play();
        if (skipIntro)
        {
            blackScreen.GetComponent<CanvasGroup>().alpha = 0;
            yield return new WaitForSeconds(2f);
            OnIntroFinish?.Invoke();
            yield break;
        }
        

        StartCoroutine(ThunderAudio());

        blackScreen.SetActive(true);
        vhsAudio.SetActive(false);

        Audio.Instance.PlayAudioFadeIn(rainAudio, 0.01f, true);

        yield return new WaitForSeconds(3f);

        HintMessage.Instance.SetMessage(hintMsg);

        for (int i = 0; i < introText.Length; i++)
        {
            yield return StartCoroutine(TypeText(introText[i]));
            if(i == introText.Length-1)
            {
                Debug.Log("last text");
                footstepsSource.Stop();
            }
            float elapsed = 0f;
            while (elapsed < showTextDuration)
            {
                //move to the next sentence;
                if (!sentenceCompleted) break;
                elapsed += Time.deltaTime;
                yield return null;
            }
        }
        
        introTextUI.gameObject.SetActive(false);
        HintMessage.Instance.RemoveMessage();
        EntranceDoorAudio();
        yield return new WaitForSeconds(5f);

        //Display objective after monologue;
        ObjectiveManager.Instance.ShowObjective(objectiveText);

        audioSource.clip = cameraAudio;
        audioSource.Play();

        yield return new WaitForSeconds(0.5f);

        vhsAudio.SetActive(true);
        blackScreen.GetComponent<CanvasGroup>().DOFade(0,1f);
        recTimer.StartTimer();

        Audio.Instance.FadeOut();

        StopTypingAudio.Raise();
        OnIntroFinish?.Invoke();
        //EnablePlayerCamera?.Invoke();
    }

    private IEnumerator ThunderAudio()
    {
        while (this.gameObject.activeSelf) // Infinite loop to keep playing the sound
        {
            thunderSource.Play();
            yield return new WaitForSeconds(12f);
        }
    }


    private void EntranceDoorAudio()
    {
        audioSource.clip = entranceAudio;
        audioSource.Play();
    }
}
