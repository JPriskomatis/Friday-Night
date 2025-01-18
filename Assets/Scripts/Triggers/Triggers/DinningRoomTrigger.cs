using GlobalSpace;
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
        [SerializeField] private Transform player;
        private Animator anim;

        [Header("Monster Settings")]
        [SerializeField] private GameObject monster;
        [SerializeField] private Transform wayPoint;
        [SerializeField] private float speed;

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
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
            int count = 3;
            while (count > 0)
            {
                Debug.Log(count);  // Logs the countdown number to the console
                yield return new WaitForSeconds(1);  // Waits for 1 second
                count--;
            }
            Debug.Log("Countdown Complete!");
            anim.SetTrigger("open");

            //Monster Activity;
            StartMonsterActivity();
        }

        private void StartMonsterActivity()
        {
            monster.SetActive(true);
        }
    }

}