using System.Collections;
using AudioSpace;
using MonsterSpace;
using TriggerSpace;
using UnityEngine;

public class FinalRoomTrigger : FloorTrigger
{
    [SerializeField] private GameObject monsterSpawnPoint;
    [SerializeField] private Jumpscare jumpscare;
    [SerializeField] private AudioClip jumpscareClip;

    private GameObject spawnedMonster;
    private Camera camera;

    private void Start()
    {
        camera = Camera.main;
    }
    protected override void InitiateAction()
    {
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
        yield return new WaitForSeconds(0f);
        jumpscare.InitiateJumpscare();
        //Jumpscare Audio;
        //Audio.Instance.PlayAudio(jumpscareClip);

        //PlayerCamera.Instance.InitiateGlitchEffect();
        Destroy(spawnedMonster.gameObject, 0.5f);

        Debug.Log("END GAME");
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
