using GlobalSpace;
using UnityEngine;

namespace MonsterSpace
{
    public class SpawnManager : Singleton<SpawnManager>
    {
        [SerializeField] private GameObject[] spawnPoints;
        [SerializeField] private GameObject monster;
        private GameObject instantiatedMonster;

        public GameObject GetMonster()
        {
            return instantiatedMonster;
        }

        public void SpawnMonster(GameObject spawnPoint)
        {
            instantiatedMonster = Instantiate(monster, spawnPoint.transform);
        }




    }

}