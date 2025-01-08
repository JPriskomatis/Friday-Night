using DG.Tweening;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    [SerializeField] private GameObject startGamePanel;
    private Animator anim;
    private CanvasGroup blackPanel;

    private void Start()
    {
        anim = Camera.main.gameObject.GetComponent<Animator>();
        blackPanel = Camera.main.GetComponentInChildren<CanvasGroup>();
    }
    public void StartGamePanel()
    {
        startGamePanel.SetActive(true);
    }

    public void EnterGame()
    {
        anim.SetTrigger("Enter");
        blackPanel.DOFade(1, 0.5f);
    }
}
