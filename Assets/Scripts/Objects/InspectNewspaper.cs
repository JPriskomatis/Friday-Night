using EJETAGame;
using GlobalSpace;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

namespace ObjectSpace
{
    public class InspectNewspaper : MonoBehaviour, IInteractable
    {
        [Header("Newspaper Text")]
        [SerializeField] private string[] newspaperText;
        [SerializeField] private bool canInteractWith;

        private void Start()
        {
            canInteractWith = true;
        }

        public void Interact()
        {
            if (Input.GetKeyUp(GlobalConstants.INTERACTION) && canInteractWith)
            {
                canInteractWith = false;
                StartCoroutine(ReadNewspaper());
            } 
        }

        public void OnInteractEnter()
        {
            if(canInteractWith)
                PlayerThoughts.Instance.SetText("Read Newspaper");
        }

        public void OnInteractExit()
        {
            
        }

        IEnumerator ReadNewspaper()
        {
            for (int i = 0; i < newspaperText.Length; i++)
            {
                PlayerThoughts.Instance.SetText(newspaperText[i]);
                yield return new WaitForSeconds(PlayerThoughts.Instance.showTextDuration);

            }
            canInteractWith = true;
        }

    }

}