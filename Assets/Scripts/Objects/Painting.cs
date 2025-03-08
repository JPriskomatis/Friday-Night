using GlobalSpace;
using ObjectiveSpace;
using System;
using System.Collections;
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


        private void DropPainting()
        {
            anim.SetTrigger("Drop");
            
            glassShattered.SetActive(true);
            StartCoroutine(WhatWasThat());
        }

        IEnumerator WhatWasThat()
        {
            yield return new WaitForSeconds(2f);
            PlayerThoughts.Instance.SetText("What was that");
        }

        public void ShatteredAudio()
        {
            audioSource.Play();
        }
    }

}