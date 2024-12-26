using EJETAGame;
using GlobalSpace;
using ObjectiveSpace;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

namespace ObjectSpace
{
    public class InspectNewspaper : InteractableItem
    {
        [Header("Newspaper Text")]
        [SerializeField] private string[] newspaperText;

        private void OnEnable()
        {
            FirstObjective.OnDropPainting += EnableInteraction;
        }

        private void OnDisable()
        {
            FirstObjective.OnDropPainting -= EnableInteraction;
        }
        private void Start()
        {
            canInteractWith = false;
        }

        private void EnableInteraction()
        {
            canInteractWith = true;
        }
        IEnumerator ReadNewspaper()
        {
            PlayerThoughts.Instance.showTextDuration = 3f;
            for (int i = 0; i < newspaperText.Length; i++)
            {
                PlayerThoughts.Instance.SetText(newspaperText[i]);
                yield return new WaitForSeconds(PlayerThoughts.Instance.showTextDuration+0.5f);

            }
            canInteractWith = true;
        }

        protected override void BeginInteraction()
        {
            StartCoroutine(ReadNewspaper());
            canInteractWith = false;
        }
    }

}