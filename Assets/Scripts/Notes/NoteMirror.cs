using UnityEngine;
using UnityEngine.Events;

public class NoteMirror : MonoBehaviour
{
    //Called automatically when note gets picked up;
    [SerializeField] private GameObject bloodAppears;
    public void PickedUpNote()
    {
        bloodAppears.SetActive(true);
    }
}
