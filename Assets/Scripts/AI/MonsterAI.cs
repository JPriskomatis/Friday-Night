using ObjectSpace;
using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace AISpace
{
    public class MonsterAI : MonoBehaviour
    {
        //Target Components
        private Transform player;
        private Transform target;

        [Header("AI Components")]
        [SerializeField] private float speed;
        [SerializeField] private Transform waypoint;
        Animator anim;
        private Vector3 previousPosition;

        private Coroutine moveCoroutine;


        private void Start()
        {
            anim = GetComponent<Animator>();
            player = GameObject.FindGameObjectWithTag("Player").transform;
            previousPosition = transform.position;
            StartMovement();

        }
        private void Update()
        {
            Vector3 targetPostition = new Vector3(target.position.x,

            this.transform.position.y,
                                       target.position.z);
            this.transform.LookAt(targetPostition);

            transform.LookAt(target);

            if (transform.position != previousPosition)
            {
                anim.SetBool("IsMoving", true);
            }
            else
            {
                anim.SetBool("IsMoving", false);
            }

            previousPosition = transform.position;
        }
        private void StartMovement()
        {
            if (Hide.isHiding)
            {
                if (moveCoroutine == null)
                {
                    moveCoroutine = StartCoroutine(MoveTowardsWaypoint());
                }
            }
            else
            {
                if (moveCoroutine == null)
                {
                    moveCoroutine = StartCoroutine(MoveTowardsPlayer());
                }
            }
        }

        private IEnumerator MoveTowardsWaypoint()
        {
            target = waypoint;
            while (Hide.isHiding)
            {
                transform.position = Vector3.MoveTowards(transform.position, waypoint.position, speed * Time.deltaTime);
                yield return null;
            }
            moveCoroutine = null; 
            StartMovement();
        }

        private IEnumerator MoveTowardsPlayer()
        {
            target = player;
            while (!Hide.isHiding)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.position.x, transform.position.y, player.position.z), speed * Time.deltaTime);
                yield return null;
            }
            moveCoroutine = null;
            StartMovement();
        }

    }
}
