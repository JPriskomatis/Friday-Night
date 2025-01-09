using GlobalSpace;
using UnityEngine;

namespace AudioSpace
{
    public class Audio : Singleton<Audio>
    {
        [SerializeField] private AudioSource audioSource;
        public void PlayAudio(AudioClip clip)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }

}