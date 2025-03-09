using UnityEngine;
using UnityEngine.Events;



/// <summary>
/// We want a game object to "listen" to multiple events, for example, a gameobject with an audio source
/// might want to play Audio when something happens, and stop audio when something else happens;
/// 
/// We create a serializable structure that holds the GameEvent we listen to,
/// and the UnityEvent the response will be;
/// </summary>
[System.Serializable]
public class GameEventResponse
{
    public GameEvent Event;     
    public UnityEvent Response; 
}

public class GameEventListener : MonoBehaviour
{
    //Each gameobject can have multiple event responses;
    public GameEventResponse[] EventResponses;

    private void OnEnable()
    {
        //We subscribe each event using their RegisterListener function
        for (int i = 0; i < EventResponses.Length; i++)
        {
            EventResponses[i].Event.RegisterListener(this);
        }
    }

    private void OnDisable()
    {
        //We unsibscribe each event using their UnregisterListener function
        for (int i = 0; i < EventResponses.Length; i++)
        {
            EventResponses[i].Event.UnregisterListener(this);
        }
    }

    //Gets called when the event we have subscribed to, gets called;
    public void OnEventRaised(GameEvent raisedEvent)
    {
        //we find the corresponding Response for the specific event;
        for (int i = 0; i < EventResponses.Length; i++)
        {
            if (EventResponses[i].Event == raisedEvent)
            {

                EventResponses[i].Response?.Invoke();
                break;
            }
        }
    }

}
