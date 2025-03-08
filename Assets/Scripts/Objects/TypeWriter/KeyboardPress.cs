using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace ObjectSpace
{
    public class KeyboardPress : MonoBehaviour
    {
        [SerializeField]
        private KeyCode[] keyCode = new KeyCode[]
        {
            KeyCode.Alpha0, KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9,
            KeyCode.A, KeyCode.B, KeyCode.C, KeyCode.D, KeyCode.E, KeyCode.F, KeyCode.G, KeyCode.H, KeyCode.I, KeyCode.J, KeyCode.K, KeyCode.L,
            KeyCode.M, KeyCode.N, KeyCode.O, KeyCode.P, KeyCode.Q, KeyCode.R, KeyCode.S, KeyCode.T, KeyCode.U, KeyCode.V, KeyCode.W, KeyCode.X,
            KeyCode.Y, KeyCode.Z
        };

        [SerializeField] private Image[] buttonSprites;
        [SerializeField] private Color pressedColor = Color.green;
        [SerializeField] private Color normalColor = Color.white;

        private Dictionary<KeyCode, Image> keyToImageMap;

        void Start()
        {
            if (keyCode.Length != buttonSprites.Length)
            {
                Debug.LogError("KeyCode array and ButtonSprite array length do not match.");
                return;
            }

            // Initialize the dictionary to map KeyCode to the corresponding Image (Button Sprite)
            keyToImageMap = new Dictionary<KeyCode, Image>();
            for (int i = 0; i < keyCode.Length; i++)
            {
                keyToImageMap[keyCode[i]] = buttonSprites[i];
            }
        }

        void Update()
        {
            // Check for key presses and update button colors accordingly
            foreach (KeyCode key in keyCode)
            {
                if (Input.GetKeyDown(key))
                {
                    OnKeyPress(key);
                }
            }
        }

        private void OnKeyPress(KeyCode pressedKey)
        {
            // Check if the key exists in the dictionary
            if (keyToImageMap.TryGetValue(pressedKey, out Image buttonImage))
            {
                // Change the color of the button image when the key is pressed
                buttonImage.color = pressedColor;

                // Optionally reset to the normal color after a short delay
                StartCoroutine(ResetButtonColor(buttonImage));
            }
        }

        private IEnumerator ResetButtonColor(Image buttonImage)
        {
            yield return new WaitForSeconds(0.2f); // Adjust delay time as needed
            buttonImage.color = normalColor;
        }
    }
}
