using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace MonsterSpace
{
    public class MonsterSpawn : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private GameObject monsterHead;



        private void Update()
        {
            LookAtPlayer();
        }
        private void LookAtPlayer()
        {
            Vector3 targetPostition = new Vector3(target.position.x,
            this.transform.position.y,
                                       target.position.z);
            this.transform.LookAt(targetPostition);
        }
    }

}