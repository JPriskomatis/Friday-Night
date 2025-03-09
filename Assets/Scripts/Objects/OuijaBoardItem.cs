using AudioSpace;
using EJETAGame;
using GlobalSpace;
using PlayerSpace;
using System.Collections;
using UISpace;
using UnityEngine;
using UnityEngine.Events;
using VoiceSpace;

namespace ObjectSpace
{
    public class OuijaBoardItem : InteractableItem
    {
        [Header("Extra Components")]
        [SerializeField] private Component voiceRecScript;
        [SerializeField] private string hintMessage;
        [SerializeField] private string findLighterTxt;
        [SerializeField] private GameObject candleLight;
        [SerializeField] private AudioClip lighterClip;
        public UnityEvent OnUseOuija;

        [Header("Move to Position Settings")]
        [SerializeField] Transform targetTransform;
        [SerializeField] float speed;

        [SerializeField] private bool hasLighter;
        private bool candleLit;

        private void OnEnable()
        {
            OuijaBoardVoice.OnOuijaJumpscare += EscapeOuija;
        }

        private void OnDisable()
        {
            OuijaBoardVoice.OnOuijaJumpscare -= EscapeOuija;
        }

        protected override void BeginInteraction()
        {
            if (LighterDesk.lighterAcquired || hasLighter)
            {
                //MovePlayer;
                PlayerController.Instance.MoveToPosition(targetTransform, speed);

                //We do this to enable/disable the script of voice recognition;
                ((MonoBehaviour)voiceRecScript).enabled = true;
                InteractionText.instance.SetText("");
                HintMessage.Instance.SetMessage(hintMessage);

                //Light candle;
                if (!candleLit)
                {
                    StartCoroutine(LightCandle());
                    candleLit = true;
                }

                OnUseOuija?.Invoke();
                OnUseOuija = null;
            }
            else
            {
                PlayerThoughts.Instance.SetText(findLighterTxt);
                canInteractWith = true;
            }
            
            
        }

        private IEnumerator LightCandle()
        {
            Audio.Instance.PlayAudio(lighterClip);
            yield return new WaitForSeconds(0.8f);

            candleLight.SetActive(true);
        }

        private void Update()
        {
            if (Input.GetKeyDown(GlobalConstants.ESCAPE_ACTION))
            {
                EscapeOuijaTemporary();
            } 
        }
        private void EscapeOuijaTemporary()
        {
            canInteractWith = true;
            PlayerController.Instance.ResetMovement();
            ((MonoBehaviour)voiceRecScript).enabled = false;
            HintMessage.Instance.RemoveMessage();
        }

        public void EscapeOuija()
        {
            canInteractWith = false;
            PlayerController.Instance.ResetMovement();
            ((MonoBehaviour)voiceRecScript).enabled = false;
            HintMessage.Instance.RemoveMessage();
        }

    }

}