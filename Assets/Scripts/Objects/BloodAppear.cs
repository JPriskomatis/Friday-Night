using EJETAGame;
using PlayerSpace;
using System.Collections;
using UnityEngine;

namespace ObjectSpace
{
    public class BloodAppear : InteractableItem
    {

        [SerializeField] private Transform targetPos;
        [SerializeField] private float speed;

        [Header("Extra Components")]
        [SerializeField] private Component voiceRecScript;


        protected override void BeginInteraction()
        {
            //Move player to position;
            PlayerMovement.Instance.MoveToPosition(targetPos, speed);
            InteractionText.instance.SetText("");

            ((MonoBehaviour)voiceRecScript).enabled = true;

        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                ((MonoBehaviour)voiceRecScript).enabled = false;
                PlayerMovement.Instance.ResetMovement();
            }
        }

    }

}