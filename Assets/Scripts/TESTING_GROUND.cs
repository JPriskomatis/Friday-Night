using GlobalSpace;
using System;
using UnityEngine;

namespace TestSpace
{
    public class TESTING_GROUND : MonoBehaviour
    {
        public static event Action<String> OnEnableObjective;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log(other.name);
                //PlayerThoughts.Instance.SetText("How did I get here");
                OnEnableObjective?.Invoke("Find the missing painting in the office");
            }
        }
    }
}