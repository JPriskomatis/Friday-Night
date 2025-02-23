using System;
using UnityEngine;

public class VoiceButtonsSetting : MonoBehaviour
{
    public static event Action OnEnableButtons;
    public static event Action OnDisableButtons;

    public static event Action OnPause;
    public static event Action OnResume;

    [SerializeField] private GameObject uiPanel;
    [SerializeField] private GameObject checkmark;
    private void Start()
    {
        if (GameSettings.VOICE_REC)
        {
            Debug.Log("voice is active");
            OnEnableButtons?.Invoke();
        } else
        {
            Debug.Log("voice is Not Active");
            OnDisableButtons?.Invoke();
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            uiPanel.SetActive(!uiPanel.activeSelf);
            if(uiPanel.activeSelf )
            {
                OnPause?.Invoke();
            }
            else
            {
                OnResume?.Invoke();
            }
        }
    }

    public void EnableVoiceRec()
    {
        checkmark.SetActive(!checkmark.activeSelf);
        if(checkmark.activeSelf)
        {
            GameSettings.VOICE_REC = true;
            OnEnableButtons?.Invoke();
        }
        else
        {
            GameSettings.VOICE_REC= false;
            OnDisableButtons?.Invoke();
        }
    }
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Y))
    //    {
    //        OnEnableButtons?.Invoke();
    //        Debug.Log("first");
    //    }
    //    if (Input.GetKeyDown(KeyCode.X))
    //    {
    //        OnDisableButtons?.Invoke();
    //    }
    //}
}
