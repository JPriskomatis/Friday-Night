using UnityEngine;

namespace MonsterSpace
{
    public class CollideWithMonster : MonoBehaviour
    {
        public GameEvent CollideMonster;
        public TargetToFollow targetGameObject;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log(this.gameObject.name);
                targetGameObject.SetTarget(this.gameObject);

                CollideMonster.Raise();
                Debug.Log("Player");
            }
        }
    }

}