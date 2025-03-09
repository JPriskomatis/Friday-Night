using UnityEngine;
using UnityEngine.Events;

public class NoteMirror : MonoBehaviour
{
    //Called automatically when note gets picked up;
    [SerializeField] private GameObject bloodAppears;
    public GameEvent ObjectiveNote;

    public void PickedUpNote()
    {
        bloodAppears.SetActive(true);
    }
    public void RaiseObjectiveNote()
    {
        ObjectiveNote.Raise();
        Debug.Log("HERE");
    }


}
