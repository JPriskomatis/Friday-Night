using System;
using TMPro;
using UnityEngine;
using System.Collections;

namespace UISpace
{
    public class RecTimer : MonoBehaviour
    {
        [Header("Text Timer")]
        public TextMeshProUGUI timerText;
        private int totalSeconds = 0;

        private Coroutine timerCoroutine;

        public static event Action OnFirstObjective;
        public bool startCounting;

        #region FOR TESTING VARIABLES
        public bool skipFirstObjective;
        #endregion

        public void StartTimer()
        {
            if (timerCoroutine == null) // Ensure we don't start multiple coroutines
            {
                timerCoroutine = StartCoroutine(TimerRoutine());
            }
        }

        private IEnumerator TimerRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f); // Wait for 1 second
                totalSeconds++;
                UpdateTimerText();

                if (totalSeconds == 60 || skipFirstObjective)
                {
                    OnFirstObjective?.Invoke();
                    yield break; // Stop the coroutine once the first objective is triggered
                }
            }
        }

        public void UpdateTimerText()
        {
            int hours = totalSeconds / 3600;
            int minutes = (totalSeconds % 3600) / 60;
            int seconds = totalSeconds % 60;

            timerText.text = $"{hours:D2}:{minutes:D2}:{seconds:D2}";
        }
    }
}
