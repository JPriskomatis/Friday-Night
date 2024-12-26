using DG.Tweening;
using GlobalSpace;
using System.Collections;
using UnityEngine;

public class EnableBlackScreen : Singleton<EnableBlackScreen>
{
    [SerializeField] private GameObject blackScreen;
    [SerializeField] private CanvasGroup blackScreenCanvas;
    [SerializeField] private AudioSource audioSource;

    public void StartBlackScreen(AudioClip audioclip)
    {
        StartCoroutine(FadeInScreen(audioclip));
    }
    IEnumerator FadeInScreen(AudioClip audioclip)
    {
        audioSource.clip = audioclip;
        //ensurre the blackscreen stats from being fully transparent;
        blackScreenCanvas.alpha = 0;
        blackScreen.SetActive(true);

        //fade in the black screen by making it visible;
        blackScreenCanvas.DOFade(1, 1f).OnComplete(() => {
            audioSource.Play();
        });

        yield return new WaitForSeconds(2f);

        FadeOutScreen();
    }

    public void FadeOutScreen()
    {
        //ensurre the blackscreen stats from being fully transparent;
        blackScreenCanvas.alpha = 1;
        blackScreen.SetActive(true);

        //fade in the black screen by making it visible;
        blackScreenCanvas.DOFade(0, 1f);
    }
}
