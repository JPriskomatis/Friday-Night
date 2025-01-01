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
        private GameObject spawnedMonster;

        protected override void InitiateAction()
        {
            //Spawn monster in the correct position;
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
                    Debug.Log("Found target.");

                    StartCoroutine(DelayMethod());
                }
                else
                {
                    Debug.Log("Player is no longer looking at the target.");
                }

                yield return null; // Wait for the next frame
            }
        }

        private bool IsPlayerLookingAt(GameObject target)
        {
            RaycastHit hit;
            if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit))
            {
                if (hit.collider.gameObject.CompareTag("Monster"))
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
        }
    }

}