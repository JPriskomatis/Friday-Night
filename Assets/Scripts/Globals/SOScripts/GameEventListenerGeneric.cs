using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class GameEventResponseGeneric<T>
{
    public GameEventBase<T> Event;
    public UnityEvent<T> Response;
}

public class GameEventListenerGeneric<T> : MonoBehaviour
{
    public GameEventResponseGeneric<T>[] EventResponses;

    private void OnEnable()
    {
        foreach (var response in EventResponses)
        {
            response.Event.RegisterListener(this);
        }
    }

    private void OnDisable()
    {
        foreach (var response in EventResponses)
        {
            response.Event.UnregisterListener(this);
        }
    }

    public void OnEventRaised(T value)
    {
        foreach (var response in EventResponses)
        {
            response.Response?.Invoke(value);
        }
    }
}
