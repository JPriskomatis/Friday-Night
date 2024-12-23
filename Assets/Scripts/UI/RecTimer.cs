using System;
using TMPro;
using UnityEngine;

namespace UISpace
{
    public class RecTimer : MonoBehaviour
    {
        [Header("Text Timer")]
        public TextMeshProUGUI timerText;
        private int totalSeconds = 0;  

        private float lastUpdateTime = 0f;

        public static event Action OnFirstObjective;

        void Start()
        {
            UpdateTimerText();
        }

        void Update()
        {
            if (Time.time >= lastUpdateTime + 1f)
            {
                lastUpdateTime += 1f;
                totalSeconds++;       
                UpdateTimerText();
            }
            if(totalSeconds == 20)
            {
                OnFirstObjective?.Invoke();
            }
        }

        void UpdateTimerText()
        {
            int hours = totalSeconds / 3600;
            int minutes = (totalSeconds % 3600) / 60;
            int seconds = totalSeconds % 60;

            timerText.text = $"{hours:D2}:{minutes:D2}:{seconds:D2}";
        }
    }

}