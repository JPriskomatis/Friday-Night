using System;
using UISpace;
using UnityEngine;

namespace ObjectiveSpace
{
    public class FirstObjective : MonoBehaviour
    {
        private float timeDuration;
        private float timeRemaining;
        private bool timerRunning;

        public static event Action OnDropPainting;

        private void OnEnable()
        {
            RecTimer.OnFirstObjective += ActivateFirstEvent;
        }

        private void OnDisable()
        {
            RecTimer.OnFirstObjective -= ActivateFirstEvent;
        }
        private void ActivateFirstEvent()
        {
            //I unregister the event listener here because I want it to be called only once, and since im counting
            //my seconds in the Update, each frame is about 153 calls;
            RecTimer.OnFirstObjective -= ActivateFirstEvent;

            //Drop the painting;
            OnDropPainting?.Invoke();
        }

    }

}