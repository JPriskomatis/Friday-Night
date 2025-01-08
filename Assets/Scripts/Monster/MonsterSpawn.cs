using UnityEngine;


namespace MonsterSpace
{
    public class MonsterSpawn : MonoBehaviour
    {
        private Transform target;
        [SerializeField] private GameObject monsterHead;

        private void Start()
        {
            target = GameObject.FindGameObjectWithTag("PlayerHead").transform;
        }

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

            monsterHead.transform.LookAt(target);
        }
    }

}