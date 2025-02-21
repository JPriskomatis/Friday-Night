using AudioSpace;
using EJETAGame;
using GlobalSpace;
using PlayerSpace;
using System.Collections;
using UISpace;
using UnityEngine;
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

        [Header("Move to Position Settings")]
        [SerializeField] Transform targetTransform;
        [SerializeField] float speed;

        [SerializeField] private bool hasLighter;

        private void OnEnable()
        {
            LighterDesk.OnGrabLighter += HasLighter;
            OuijaBoardVoice.OnOuijaJumpscare += EscapeOuija;
        }

        private void OnDisable()
        {
            LighterDesk.OnGrabLighter -= HasLighter;
            OuijaBoardVoice.OnOuijaJumpscare -= EscapeOuija;
        }
        private void HasLighter()
        {
            hasLighter = true;
        }
        protected override void BeginInteraction()
        {
            if (hasLighter)
            {
                //MovePlayer;
                PlayerController.Instance.MoveToPosition(targetTransform, speed);

                //We do this to enable/disable the script of voice recognition;
                ((MonoBehaviour)voiceRecScript).enabled = true;
                InteractionText.instance.SetText("");
                HintMessage.Instance.SetMessage(hintMessage);

                //Light candle;
                StartCoroutine(LightCandle());
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

                //TESTING
                hasLighter = true;
                //END
                EscapeOuija();
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