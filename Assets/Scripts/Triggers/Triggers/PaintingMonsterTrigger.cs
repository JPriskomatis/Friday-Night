using System.Collections;
using AudioSpace;
using MonsterSpace;
using PlayerSpace;
using UnityEngine;

namespace TriggerSpace
{
    public class PaintingMonsterTrigger : FloorTrigger
    {
        [SerializeField] private GameObject monsterSpawnPoint;
        [SerializeField] private Camera camera;
        [SerializeField] private AudioClip jumpscareClip;

        [SerializeField] private Jumpscare jumpscare;

        private GameObject spawnedMonster;

        private void Start()
        {
            interactAgain = false;
            interactable = false;
        }

        private void OnEnable()
        {
            PaintingsTrigger.OnSetCollider += ActivateCollider;
        }
        private void OnDisable()
        {
            PaintingsTrigger.OnSetCollider -= ActivateCollider;
        }

        private void ActivateCollider()
        {
            interactable = true;
        }
        protected override void InitiateAction()
        {
            Debug.Log("yes");
            //We spawn the monster;
            SpawnManager.Instance.SpawnMonster(monsterSpawnPoint);

            
            StartCoroutine(CheckPlayerLookingAt());


        }

        private IEnumerator CheckPlayerLookingAt()
        {
            spawnedMonster = SpawnManager.Instance.GetMonster();

            float checkDuration = 10f; // Check for 10 seconds
            float elapsedTime = 0f;
            while (elapsedTime < checkDuration)
            {
                if (IsPlayerLookingAt(spawnedMonster))
                {

                    StartCoroutine(DelayMethod());
                }
                else
                {
                }

                yield return null; // Wait for the next frame
            }
        }

        IEnumerator DelayMethod()
        {
            yield return new WaitForSeconds(0.25f);
            jumpscare.InitiateJumpscare();
            //Jumpscare Audio;
            Audio.Instance.PlayAudio(jumpscareClip);

            //PlayerCamera.Instance.InitiateGlitchEffect();
            Destroy(spawnedMonster.gameObject,0.5f);
            Destroy(this);
        }

        private bool IsPlayerLookingAt(GameObject target)
        {
            RaycastHit hit;
            if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit))
            {
                if (hit.collider.gameObject.layer == 7)
                {

                    return true;
                }
            }
            else
            {
                Debug.Log("Target not found.");
            }
            return false;
        }
    }

}