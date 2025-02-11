using UnityEngine;

namespace ObjectSpace
{
    public class DoorAudio : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        public void PlayDoorAudio()
        {
            audioSource.Play();
        }
    }

}