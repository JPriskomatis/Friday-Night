using DG.Tweening;
using UnityEngine;

namespace MonsterSpace
{
    public class Mannequin : MonoBehaviour
    {
        [SerializeField] private Transform moveToPosition;
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                MoveToPosition();
            }
        }
        public void MoveToPosition()
        {
            this.transform.DOMove(new Vector3(moveToPosition.position.x, moveToPosition.position.y, moveToPosition.position.z), 0f);
            transform.Rotate(0, 90, 0);
        }
    }
}