using UnityEngine;

namespace NoteSpace
{
    [CreateAssetMenu(fileName ="Note", menuName ="Notes/Create New Note")]
    public class NoteSO : ScriptableObject
    {
        public string title;
        public Material mat;
        public string description;
    }

}