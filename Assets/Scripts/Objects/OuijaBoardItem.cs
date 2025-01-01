using EJETAGame;
using GlobalSpace;
using System.Collections;
using UnityEngine;
using VoiceSpace;

namespace ObjectSpace
{
    public class OuijaBoardItem : InteractableItem
    {
        [Header("Extra Components")]
        [SerializeField] private Component voiceRecScript;
        [SerializeField] private OuijaBoard ouijaBoard;

        [Header("Move to Position Settings")]
        [SerializeField] Transform playerTransform;
        [SerializeField] Transform targetTransform;
        [SerializeField] float speed;
        CharacterController characterController;


        private void Start()
        {
            characterController = playerTransform.GetComponent<CharacterController>();
        }
        protected override void BeginInteraction()
        {
            //MovePlayer;
            StartCoroutine(MovePlayer());
            ((MonoBehaviour)voiceRecScript).enabled = true;
            InteractionText.instance.SetText("");
            
        }
        private IEnumerator MovePlayer()
        {
            while (Vector3.Distance(playerTransform.position, targetTransform.position) > 0.3f)
            {
                Vector3 direction = (targetTransform.position - playerTransform.position).normalized;
                characterController.Move(direction * speed * Time.deltaTime);
                yield return null;
            }
            Debug.Log("end");
            characterController.enabled = false;
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                characterController.enabled = true;
                ((MonoBehaviour)voiceRecScript).enabled = false;
                canInteractWith = true;
            } 
        }
    }

}