using UnityEngine;

namespace NoteSpace
{
    [CreateAssetMenu(fileName ="Note", menuName ="Notes/Create New Note")]
    public class NoteSO : ScriptableObject
    {
        public string title;
        [TextArea(5, 15)]
        public string description;
        public Material material;
    }

}