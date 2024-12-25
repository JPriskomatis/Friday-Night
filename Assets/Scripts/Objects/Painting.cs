using ObjectiveSpace;
using System;
using UnityEngine;

namespace ObjectSpace
{
    public class Painting : MonoBehaviour
    {
        [Header("Painting Components")]
        [SerializeField] private Animator anim;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private GameObject glassShattered;
        private void OnEnable()
        {
            FirstObjective.OnDropPainting += DropPainting;
        }

        private void OnDisable()
        {
            FirstObjective.OnDropPainting -= DropPainting;
        }

        //TESTING
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                DropPainting();
            }
        }

        private void DropPainting()
        {
            anim.SetTrigger("Drop");
            
            glassShattered.SetActive(true);
        }

        public void ShatteredAudio()
        {
            audioSource.Play();
        }
    }

}