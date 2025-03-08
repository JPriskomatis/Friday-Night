using System.Collections;
using DG.Tweening;
using GlobalSpace;
using UISpace;
using UnityEngine;

public class VoiceHintUI : MonoBehaviour
{
    [SerializeField] private GameObject voicePanel;
    [SerializeField] private CanvasGroup voiceCanvasGroup;


    private void Start()
    {
        StartCoroutine(ActivateVoiceHint());
        StartCoroutine(CheckForXKey());
    }

    IEnumerator ActivateVoiceHint()
    {
        voiceCanvasGroup.DOFade(1, 1f);
        yield return new WaitForSeconds(20f);
        voiceCanvasGroup.DOFade(0, 1f).OnComplete(() => voicePanel.SetActive(false));
        Destroy(gameObject, 2f);



    }
    private IEnumerator CheckForXKey()
    {
        yield return new WaitForSeconds(1f);
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                voiceCanvasGroup.DOFade(0, 1f).OnComplete(() => voicePanel.SetActive(false));
                Destroy(gameObject, 2f);
            }
            yield return null;
        }
    }

}
