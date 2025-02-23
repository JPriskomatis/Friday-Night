using System;
using UnityEngine;

public class VoiceButtonsSetting : MonoBehaviour
{
    public static event Action OnEnableButtons;
    public static event Action OnDisableButtons;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            OnEnableButtons?.Invoke();
            Debug.Log("first");
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            OnDisableButtons?.Invoke();
        }
    }
}
