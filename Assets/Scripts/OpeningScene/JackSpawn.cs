using System.Collections;
using UnityEngine;

namespace OpeningScene
{
    public class JackSpawn : MonoBehaviour
    {
        [SerializeField] private SkinnedMeshRenderer mesh;
        private void OnEnable()
        {
            LightFlicker.OnSpawnJack += SpawnJack;
        }

        private void OnDisable()
        {
            LightFlicker.OnSpawnJack -= SpawnJack;
        }

        private void SpawnJack()
        {
            mesh.enabled = true;
            StartCoroutine(DelayToActivate());
        }

        IEnumerator DelayToActivate()
        {
            yield return new WaitForSeconds(2.5f);
            mesh.enabled = false;
        }
    }

}