using System.Collections;
using DG.Tweening;
using UISpace;
using UnityEngine;

public class EndDemo : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private GameObject endDemoUI;
    [SerializeField] private GameObject textToAppear;
    [SerializeField] private string hintMsg;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(2f);

        endDemoUI.SetActive(true);
        HintMessage.Instance.SetMessage(hintMsg);
        yield return new WaitForSeconds(2f);
        textToAppear.SetActive(true);
        yield return new WaitForSeconds(2f);
        Application.Quit();
    }

}
