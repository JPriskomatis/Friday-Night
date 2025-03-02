using System.Collections;
using TMPro;
using UnityEngine;

namespace UISpace
{
    public class TypewriterEffect : MonoBehaviour
    {
        [Header("UI Components")]
        [SerializeField] private TextMeshProUGUI introTextUI;
        [SerializeField] private string[] textList;

        [Header("Typewriter Effect")]
        [SerializeField] private float typingSpeed = 0.05f;
        private bool isTyping = false;
        [SerializeField] private float sentenceDelay;
        private bool sentenceCompleted = false;
        private int currentSentenceIndex = 0;
        private Coroutine typingCoroutine;

        private void Start()
        {
            StartNextSentence();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (isTyping)
                {
                    // Instantly complete the sentence
                    isTyping = false;
                }
                else if (sentenceCompleted)
                {
                    // Skip to next sentence if completed
                    sentenceCompleted = false;
                    StartNextSentence();
                }
            }
        }

        private void StartNextSentence()
        {
            if (currentSentenceIndex < textList.Length)
            {
                if (typingCoroutine != null)
                {
                    StopCoroutine(typingCoroutine);
                }
                typingCoroutine = StartCoroutine(TypeText(textList[currentSentenceIndex]));
                currentSentenceIndex++;
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
                    // Instantly complete the sentence
                    introTextUI.text = text;
                    break;
                }

                introTextUI.text += c;
                yield return new WaitForSeconds(typingSpeed);
            }

            isTyping = false;
            sentenceCompleted = true;

            // Auto-move to next sentence after delay if not skipped manually
            yield return new WaitForSeconds(sentenceDelay);
            if (sentenceCompleted)
            {
                StartNextSentence();
            }
        }
    }
}
