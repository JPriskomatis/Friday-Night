using System.Collections;
using GlobalSpace;
using UnityEngine;

namespace NoteSpace
{
    public class NoteEventThoughts : MonoBehaviour
    {
        public void PlayerThough(string thoughts)
        {
            PlayerThoughts.Instance.DelaySetText(thoughts);

        }
    }

}