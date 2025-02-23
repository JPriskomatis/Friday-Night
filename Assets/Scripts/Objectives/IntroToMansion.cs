using GlobalSpace;
using ObjectiveSpace;
using System.Collections;
using TMPro;
using UISpace;
using UnityEngine;

public class IntroToMansion : MonoBehaviour
{
    [Header("Intro Text Components")]
    [SerializeField] private string[] introText;
    [SerializeField] private string objectiveText;
    [SerializeField] private TextMeshProUGUI introTextUI;
    [SerializeField] private float showTextDuration;

    [Header("Audio Settings")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip entranceAudio, cameraAudio;
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
    }

    private IEnumerator Start()
    {
        if (skipIntro)
        {
            blackScreen.GetComponent<CanvasGroup>().alpha = 0;
            yield break;
        }

        blackScreen.SetActive(true);
        vhsAudio.SetActive(false);

        yield return new WaitForSeconds(3f);

        HintMessage.Instance.SetMessage(hintMsg);

        for (int i = 0; i < introText.Length; i++)
        {
            yield return StartCoroutine(TypeText(introText[i]));

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
        blackScreen.GetComponent<CanvasGroup>().alpha = 0;
        recTimer.StartTimer();
    }

    private void EntranceDoorAudio()
    {
        audioSource.clip = entranceAudio;
        audioSource.Play();
    }
}
