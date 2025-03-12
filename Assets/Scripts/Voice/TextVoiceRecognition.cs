using UnityEngine;
using UnityEngine.UI;

namespace VoiceSpace
{
    public class TextVoiceRecognition : VoiceRecognition
    {
        [SerializeField] private Button buttonImage;
        public override void AddDictionaryFunctions()
        {
            voiceActions.Add(speechWords[0], ChangeColor);
        }

        public void ChangeColor()
        {
            Debug.Log("said it");
            buttonImage.GetComponent<Image>().color = Color.red;
        }
    }

}