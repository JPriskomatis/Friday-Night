using System.Collections.Generic;
using UnityEngine;

public abstract class GameEventBase<T> : ScriptableObject
{
    private List<GameEventListenerGeneric<T>> eventListeners = new List<GameEventListenerGeneric<T>>();

    public void Raise(T value)
    {
        for (int i = eventListeners.Count - 1; i >= 0; i--)
        {
            eventListeners[i].OnEventRaised(value);
        }
    }

    public void RegisterListener(GameEventListenerGeneric<T> listener)
    {
        if (!eventListeners.Contains(listener))
        {
            eventListeners.Add(listener);
        }
    }

    public void UnregisterListener(GameEventListenerGeneric<T> listener)
    {
        if (eventListeners.Contains(listener))
        {
            eventListeners.Remove(listener);
        }
    }
}
