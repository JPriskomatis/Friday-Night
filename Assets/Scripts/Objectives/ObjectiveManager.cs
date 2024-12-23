using DG.Tweening;
using System.Collections;
using TestSpace;
using TMPro;
using UnityEngine;

namespace ObjectiveSpace
{
    public class ObjectiveManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI objectiveText;
        [SerializeField] private CanvasGroup objectiveCanvasGroup;

        private void OnEnable()
        {
            TESTING_GROUND.OnEnableObjective += ShowObjective;
        }

        private void OnDisable()
        {
            TESTING_GROUND.OnEnableObjective -= ShowObjective;
        }
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