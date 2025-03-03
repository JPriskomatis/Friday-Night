using GlobalSpace;
using ObjectSpace;
using System;
using UnityEngine;
using UnityEngine.Events;

public class ExamineObject : InteractableItem
{
    public static event Action OnExamine;
    [SerializeField] private string textToDisplay;
    [SerializeField] private bool hasEvent;
    [SerializeField] private UnityEvent eventAction;
    protected override void BeginInteraction()
    {
        PlayerThoughts.Instance.SetText(textToDisplay);
        if (hasEvent)
        {
            eventAction?.Invoke();
            Debug.Log("evoked");
        }
    }
}
