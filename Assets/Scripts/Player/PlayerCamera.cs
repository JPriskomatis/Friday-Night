using System.Collections;
using UnityEngine;

namespace PlayerSpace
{
    public class PlayerCamera : MonoBehaviour
    {
        [Header("Camera Settings")]
        [SerializeField] private Camera mainCamera;
        public string layerName = "Monster";
        int layerIndex;


        private void Start()
        {
            //Finds the specific layer we want to use;
            layerIndex = LayerMask.NameToLayer(layerName);
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                AddMonsterLayer();
            }
        }
        private void AddMonsterLayer()
        {
            mainCamera.cullingMask |= 1 << layerIndex;

            StartCoroutine(RemoveMonsterLayer());
        }

        IEnumerator RemoveMonsterLayer()
        {
            yield return new WaitForSeconds(2f);
            mainCamera.cullingMask &= ~(1 << layerIndex);
        }
    }

}