using AudioSpace;
using MonsterSpace;
using PlayerSpace;
using System.Collections;
using UnityEngine;

namespace TriggerSpace
{
    public class BathroomTrigger : FloorTrigger
    {
        [SerializeField] private GameObject monsterSpawnPoint;
        [SerializeField] private Camera camera;
        [SerializeField] private AudioSource source;
        private GameObject spawnedMonster;

        protected override void InitiateAction()
        {
            //Spawn monster in the correct position;
            SpawnManager.Instance.SpawnMonster(monsterSpawnPoint);

            //Play Audio;
            source.Play();

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

        private bool IsPlayerLookingAt(GameObject target)
        {
            RaycastHit hit;
            if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit))
            {
                if (hit.collider.gameObject.layer==7)
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

        IEnumerator DelayMethod()
        {
            yield return new WaitForSeconds(0.5f);
            PlayerCamera.Instance.InitiateGlitchEffect();
            Destroy(spawnedMonster.gameObject);
            Destroy(this);
        }
    }

}