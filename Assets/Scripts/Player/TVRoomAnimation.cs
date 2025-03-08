using UnityEngine;

namespace PlayerSpace
{
    public class TVRoomAnimation : MonoBehaviour
    {
        [SerializeField] private Transform placement;
        public void BeginAnimation()
        {
            PlayerController.Instance.ResetMovement();
            PlayerController.Instance.MoveToPosition(placement, 1f);

        }
    }

}