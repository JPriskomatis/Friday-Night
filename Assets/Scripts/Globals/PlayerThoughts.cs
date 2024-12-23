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
        public void RemoveText()
        {
            thoughtsText.gameObject.SetActive(false);
            thoughtsText.text = "";
        }

        IEnumerator DeactivateText()
        {
            yield return new WaitForSeconds(showTextDuration);
            RemoveText();
        }
    }

}