using UnityEngine;

namespace PlayerSpace
{

    public class VoiceDetection : MonoBehaviour
    {
        [SerializeField] private int sampleWindow = 64;

        public float GetMicVolume(int clipPosition, AudioClip clip)
        {
            int startPosition = clipPosition - sampleWindow;

            if(startPosition < 0)
            {
                return 0;
            }
            float[] waveDate = new float[sampleWindow];
            clip.GetData(waveDate, startPosition);

            //compute loudness;
            float totalLoudness = 0;

            for (int i = 0; i < sampleWindow; i++)
            {
                totalLoudness += Mathf.Abs(waveDate[i]);
            }

            return totalLoudness / sampleWindow;
        }
    }

}