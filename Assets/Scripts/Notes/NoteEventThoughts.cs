using GlobalSpace;
using UnityEngine;

namespace NoteSpace
{
    public class NoteEventThoughts : MonoBehaviour
    {
        public void PlayerThough()
        {
            Debug.Log("dsf");
            PlayerThoughts.Instance.SetText("Maybe I can find more notes around here");
        }
    }

}