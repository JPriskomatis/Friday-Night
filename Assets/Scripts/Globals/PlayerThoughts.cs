using System.Collections;
using TMPro;
using UnityEngine;

namespace GlobalSpace
{
    public class PlayerThoughts : Singleton<PlayerThoughts>
    {
        [SerializeField] private TextMeshProUGUI thoughtsText;
        public float showTextDuration;
        
        public void SetText(string text)
        {
            thoughtsText.gameObject.SetActive(true);
            thoughtsText.text = text;
            StartCoroutine(DeactivateText());
        }

        public void DelaySetText(string text)
        {
            StartCoroutine(DelayText(text));
            
        }
        IEnumerator DelayText(string text)
        {
            yield return new WaitForSeconds(2f);
            thoughtsText.gameObject.SetActive(true);
            thoughtsText.text = text;
            StartCoroutine(DeactivateText());
        }
        public void RemoveText()
        {
            thoughtsText.gameObject.SetActive(false);
            thoughtsText.text = "";
        }

        IEnumerator DeactivateText()
        {
            Debug.Log(showTextDuration);
            yield return new WaitForSeconds(showTextDuration);
            RemoveText();
        }
    }

}