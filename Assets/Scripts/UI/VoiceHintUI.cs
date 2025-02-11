using DG.Tweening;
using GlobalSpace;
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
        if (Input.GetKeyDown(GlobalConstants.ESCAPE_ACTION))
        {
            hint.RemoveMessage();
            voiceCanvasGroup.DOFade(0, 1f).OnComplete(() => voicePanel.SetActive(false));
            Destroy(gameObject, 2f);
        }
    }
}
