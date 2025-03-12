using UnityEngine;

public class SkipAreas : MonoBehaviour
{
    public GameEvent SkipIntro;
    [SerializeField] private bool skipIntroBool;
    private void Start()
    {
        if (skipIntroBool)
        {
            SkipIntro.Raise();

        }
    }
}
