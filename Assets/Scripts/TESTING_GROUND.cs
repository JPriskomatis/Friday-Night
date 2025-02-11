using GlobalSpace;
using ObjectiveSpace;
using System;
using UISpace;
using UnityEngine;

namespace TestSpace
{
    public class TESTING_GROUND : MonoBehaviour
    {
        RecTimer recTimer;
        IntroToMansion introToMansion;
        [SerializeField] private bool skipIntroToMansion;
        //TESTING FOR FIRST OBJECTIVE;
        private void Awake()
        {
            recTimer = FindFirstObjectByType<RecTimer>();
            introToMansion = FindFirstObjectByType<IntroToMansion>();

            introToMansion.skipIntro = skipIntroToMansion;
        }
        //private void Update()
        //{
        //    if (Input.GetKeyDown(KeyCode.Alpha0))
        //    {
        //        //Enable firstObjective action;
                
        //        recTimer.skipFirstObjective = true;

        //    }
        //}
    }
}