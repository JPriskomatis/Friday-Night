using DG.Tweening;
using UnityEngine;

namespace MonsterSpace
{
    public class Jumpscare : MonoBehaviour
    {
        [SerializeField] private CanvasGroup blackScreen;
        [SerializeField] private GameObject monster;
        public void StopController()
        {
            PlayerController.Instance.StopMovement();
            PlayerController.Instance.DisableCameraMovement();
        }

        public void EnableScreen()
        {
            blackScreen.DOFade(1, 0.2f);
            monster.SetActive(true);
        }
    }

}