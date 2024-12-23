using DG.Tweening;
using GlobalSpace;
using System.Collections;
using TMPro;
using UnityEngine;

namespace ObjectiveSpace
{
    public class ObjectiveManager : Singleton<ObjectiveManager>
    {
        [SerializeField] private TextMeshProUGUI objectiveText;
        [SerializeField] private CanvasGroup objectiveCanvasGroup;

        public void ShowObjective(string objective)
        {
            objectiveText.gameObject.SetActive(true);
            objectiveText.text = objective;
            objectiveCanvasGroup.DOFade(1, 1f);
            StartCoroutine(HideObjective());

        }

        public IEnumerator HideObjective()
        {
            yield return new WaitForSeconds(4f);
            objectiveCanvasGroup.DOFade(0, 1f).OnComplete(() => { objectiveText.gameObject.SetActive(false); });

        }

    }

}