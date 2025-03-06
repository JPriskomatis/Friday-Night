using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UISpace
{
    public class ChooseMicrophone : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI[] buttonsNames; // Existing buttons' text components
        [SerializeField] private Button[] buttons; // Existing buttons
        [SerializeField] private MicrophoneSelect microphoneSelect; // ScriptableObject reference

        private List<string> microphoneOptions = new List<string>();

        private void Start()
        {
            // Get available microphone devices
            microphoneOptions.AddRange(Microphone.devices);

            AssignButtons();
        }

        private void AssignButtons()
        {
            int count = Mathf.Min(buttonsNames.Length, buttons.Length, microphoneOptions.Count);

            for (int i = 0; i < count; i++)
            {
                buttonsNames[i].text = microphoneOptions[i]; // Set button label
                int index = i; // Prevent closure issues
                buttons[i].onClick.AddListener(() => SelectMicrophone(index));
            }
        }

        private void SelectMicrophone(int index)
        {
            if (microphoneSelect != null && index < microphoneOptions.Count)
            {
                microphoneSelect.microphone = microphoneOptions[index]; // Store the selected mic name
                Debug.Log($"Selected Microphone: {microphoneSelect.microphone}");
            }
        }
    }
}
