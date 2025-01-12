using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace MonsterSpace
{
    public class MonsterWalk : MonoBehaviour
    {
        [SerializeField] Animator anim;


        IEnumerator Start()
        {
            yield return new WaitForSeconds(2f);
            MoveMonster();
            //anim.SetTrigger("walk");
        }

        private void MoveMonster()
        {
           //this.transform.DOMove(new Vector3(272.736f, this.transform.position.y, this.transform.position.z), 5f);
            anim.SetTrigger("walk");
        }

    }

}