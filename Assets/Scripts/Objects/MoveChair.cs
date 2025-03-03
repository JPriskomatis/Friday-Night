using UnityEngine;

namespace ObjectSpace
{
    public class MoveChair : MonoBehaviour
    {
        [SerializeField] private Animator anim;
        [SerializeField] private AudioSource audioSource;
        private void OnEnable()
        {
            ExamineObject.OnExamine += StartMoveChair;
        }
        private void OnDisable()
        {
            ExamineObject.OnExamine -= StartMoveChair;
        }

        public void StartMoveChair()
        {
            //play animation;
            anim.SetTrigger("Move");

            
        }
        public void PlayAudio()
        {
            //play audio;
            audioSource.Play();
        }
    }

}