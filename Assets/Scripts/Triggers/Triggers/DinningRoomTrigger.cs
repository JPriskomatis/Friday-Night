using Codice.Client.Common.GameUI;
using GlobalSpace;
using ObjectSpace;
using System.Collections;
using UnityEngine;

namespace TriggerSpace
{
    public class DinningRoomTrigger : FloorTrigger
    {

        [Header("Text for Player")]
        [SerializeField] private string textToHide;

        [Header("Extra Components")]
        [SerializeField] private GameObject door;
        private Animator anim;

        [Header("Monster Settings")]
        [SerializeField] private GameObject monster;

        private void Start()
        {
            anim = door.GetComponent<Animator>();
        }
        protected override void InitiateAction()
        {
            //Start Knocking on Door;

            anim.SetTrigger("knock");
            //Player text to hide;
            StartCoroutine(ShowText());
            //Start Timer left to hide;
            StartCoroutine(StartCountdown());
            
        }

        IEnumerator ShowText()
        {
            yield return new WaitForSeconds(1.5f);
            PlayerThoughts.Instance.SetText(textToHide);
        }

        IEnumerator StartCountdown()
        {
            int count = 10;
            while (count > 0)
            {
                Debug.Log(count);  // Logs the countdown number to the console
                yield return new WaitForSeconds(1);  // Waits for 1 second
                count--;
            }
            Debug.Log("Countdown Complete!");
            anim.SetTrigger("open");

            //Monster Activity;
            StartCoroutine(StartMonsterActivity());
        }

        private IEnumerator StartMonsterActivity()
        {
            monster.SetActive(true);
            float duration = 10f;
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                if (HideUnderTable.isHiding)
                {
                    Debug.Log("Player is Hiding");
                } 
                else
                {
                    Debug.Log("Player is not hiding");
                    elapsedTime += 0.5f;
                    yield return new WaitForSeconds(0.5f);  // Check every 0.5 seconds
                }
                
            }
        }

    }

}