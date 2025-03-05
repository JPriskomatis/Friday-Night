using UnityEngine;

namespace ObjectSpace
{
    public class DoorAudio : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource, audioSource2;
        public void PlayDoorAudio()
        {
            audioSource.Play();
        }
        public void SlamDoor()
        {
            audioSource2.Play();
        }
    }

}