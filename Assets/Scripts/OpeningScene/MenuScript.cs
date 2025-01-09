using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    [SerializeField] private GameObject startGamePanel;
    private Animator anim;
    private CanvasGroup blackPanel;
    private bool canTransition;

    private void Start()
    {
        anim = Camera.main.gameObject.GetComponent<Animator>();
        blackPanel = Camera.main.GetComponentInChildren<CanvasGroup>();

        //Load next scene;
        StartCoroutine(LoadScene());
    }
    public void StartGamePanel()
    {
        startGamePanel.SetActive(true);
    }

    public void EnterGame()
    {
        anim.SetTrigger("Enter");
        blackPanel.DOFade(1, 0.5f);

        StartCoroutine(TransitionToScene());
    }

    IEnumerator LoadScene()
    {
        yield return null;

        AsyncOperation asyncOpe = SceneManager.LoadSceneAsync(0);
        asyncOpe.allowSceneActivation = false;

        while (!asyncOpe.isDone)
        {
            Debug.Log(asyncOpe.progress);
            if (asyncOpe.progress >= 0.9f)
            {
                canTransition = true;
                break;
            }
            yield return null;
        }
    }

    IEnumerator TransitionToScene()
    {
        if (canTransition)
        {
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }
    }
}
