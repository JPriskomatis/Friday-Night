using DG.Tweening;
using UISpace;
using UnityEngine;

public class VoiceHintUI : MonoBehaviour
{
    [SerializeField] private GameObject voicePanel;
    [SerializeField] private CanvasGroup voiceCanvasGroup;
    [SerializeField] private HintMessage hint;
    [SerializeField] private string hintTxt;

    private void Start()
    {
        hint.SetMessage(hintTxt);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            hint.RemoveMessage();
            voiceCanvasGroup.DOFade(0, 1f).OnComplete(() => voicePanel.SetActive(false));
            Destroy(gameObject, 2f);
        }
    }
}
