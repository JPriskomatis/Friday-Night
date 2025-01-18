using ObjectSpace;
using System;
using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace AISpace
{
    public class MonsterAI : MonoBehaviour
    {

        public static event Action OnPlayerCapture;
        //Target Components
        private Transform player;
        private Transform target;

        [Header("AI Components")]
        [SerializeField] private float speed;
        [SerializeField] private Transform[] waypoint;
        int waypointIndex;
        Animator anim;
        private Vector3 previousPosition;

        private Coroutine moveCoroutine;
        private bool invokeEvent;


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
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }

            if (Hide.isHiding)
            {
                moveCoroutine = StartCoroutine(MoveTowardsWaypoint());
            }
            else
            {
                moveCoroutine = StartCoroutine(MoveTowardsPlayer());
            }
        }

        private IEnumerator MoveTowardsWaypoint()
        {

            Debug.Log("Going to waypoint: " + waypointIndex);
            target = waypoint[waypointIndex];

            while (Vector3.Distance(transform.position, target.position) > 0.2f)
            {
                Debug.Log(Vector3.Distance(transform.position, target.position));
                transform.position = Vector3.MoveTowards(transform.position, waypoint[waypointIndex].position, speed * Time.deltaTime);
                yield return null;
            }
            yield return new WaitForSeconds(1f);
            waypointIndex++;
            moveCoroutine = null; 
            StartMovement();
        }

        private IEnumerator MoveTowardsPlayer()
        {
            target = player;
            while (Vector3.Distance(transform.position, target.position) > 2f)
            {
                Debug.Log(Vector3.Distance(transform.position, target.position));
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.position.x, transform.position.y, player.position.z), speed*2 * Time.deltaTime);
                yield return null;
            }
            Debug.Log("Captured Player");
            if (!invokeEvent)
            {
                OnPlayerCapture?.Invoke();
                invokeEvent = true;
                Destroy(gameObject, 1f);
            }

            moveCoroutine = null;
            //StartMovement();
        }

    }
}
