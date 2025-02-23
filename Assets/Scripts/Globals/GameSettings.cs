using UnityEngine;


public class GameSettings : MonoBehaviour
{
    public static bool VOICE_REC;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

}
