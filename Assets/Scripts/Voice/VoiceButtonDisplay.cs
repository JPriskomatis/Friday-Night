using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace VoiceSpace
{
    public class VoiceButtonDisplay : MonoBehaviour
    {
        private bool showBtns;
        [SerializeField] GameObject UIPanel;
        [SerializeField] CanvasGroup UICanvas;

        //Test Sample UI
        [SerializeField] private TextMeshProUGUI[] commandText;
        [SerializeField] private TextMeshProUGUI[] keyCountText;
        private int keyCount = 1;
        private void OnEnable()
        {
            VoiceRecognition.OnVoiceStart += DisplayButtons;
            VoiceRecognition.OnExitVoice += HideDisplayButtons;

            VoiceButtonsSetting.OnEnableButtons += EnableButtonUI;
            VoiceButtonsSetting.OnDisableButtons += DisableButtonUI;

        }

        private void OnDisable()
        {
            VoiceRecognition.OnVoiceStart -= DisplayButtons;
            VoiceRecognition.OnExitVoice -= HideDisplayButtons;

            VoiceButtonsSetting.OnEnableButtons -= EnableButtonUI;
            VoiceButtonsSetting.OnDisableButtons -= DisableButtonUI;
        }


        private void EnableButtonUI()
        {
            showBtns = true;
            UIPanel.SetActive(true);
            Debug.Log("Yesdfsdfs");
        }

        private void DisableButtonUI()
        {
            showBtns = false;
            UIPanel.SetActive(false);
        }


        private void HideDisplayButtons()
        {
            Debug.Log("second");
            UICanvas.DOFade(0, 1f);
            
        }

        private void DisplayButtons(Dictionary<string, Action> voiceCommands)
        {
            UICanvas.DOFade(1, 1f);

            SetButtonTexts(voiceCommands);
        }

        private void SetButtonTexts(Dictionary<string, Action> voiceCommands)
        {
            string[] keysArray = voiceCommands.Keys.ToArray();
            int keyCount = 1;

            // Check if there are at least 3 elements
            if (keysArray.Length >= 3)
            {
                // Add the first, middle, and last elements
                commandText[0].text = keysArray[0]; // First element
                keyCountText[0].text = "[" + keyCount++.ToString() + "]";

                commandText[1].text = keysArray[keysArray.Length / 2]; // Middle element
                keyCountText[1].text = "[" + keyCount++.ToString() + "]";

                commandText[2].text = keysArray[keysArray.Length - 1]; // Last element
                keyCountText[2].text = "[" + keyCount++.ToString() + "]";
            }
        }

    }

}