using System;
using UnityEngine;

namespace ObjectSpace
{
    public class FinalPainting : MonoBehaviour
    {
        public static event Action<Transform> OnChangeImages;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.V))
            {
                OnChangeImages?.Invoke(transform);
            }
        }
    }

}
