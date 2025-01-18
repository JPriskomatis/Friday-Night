using ObjectSpace;
using System;
using System.Collections;
using UnityEngine;

namespace AISpace
{
    public class MonsterAI : MonoBehaviour
    {
        public static event Action OnPlayerCapture;

        //Target Components;
        private Transform player;
        private Transform target;

        [Header("AI Components")]
        [SerializeField] private float speed;
        [SerializeField] private Transform[] waypoint;
        private int waypointIndex;
        private Animator anim;
        private Vector3 previousPosition;

        private Coroutine moveCoroutine;
        private bool invokeEvent;

        //Monster States;
        private enum State { chase, patrol }
        private State currentState;

        private void Start()
        {
            anim = GetComponent<Animator>();
            player = GameObject.FindGameObjectWithTag("Player").transform;
            previousPosition = transform.position;

           //Start the initial state;
            StartMovement();
        }

        private void Update()
        {
            //We want to continually check if the player is hiding;
            //TODO:
            //we can create an event system taht triggers an event architecture design instead of putting everything in update;

            bool isPlayerHiding = Hide.isHiding;

            if (isPlayerHiding && currentState != State.patrol)
            {
                //if the player is hiding and monster is not patrolling, switch to patrol statef
                currentState = State.patrol;
                StartMovement();
            }
            else if (!isPlayerHiding && currentState != State.chase)
            {
                //If the player stops hiding and monster is not chasing, switch to chase state;
                currentState = State.chase;
                StartMovement();
            }

            //Perform the current behavior based on state;
            if (currentState == State.chase)
            {
                if (moveCoroutine == null)
                    moveCoroutine = StartCoroutine(MoveTowardsPlayer());
            }
            else
            {
                if (moveCoroutine == null)
                    moveCoroutine = StartCoroutine(MoveTowardsWaypoint());
            }

            Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, target.position.z);
            transform.LookAt(targetPosition);

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
                moveCoroutine = null;
            }

            if (currentState == State.patrol)
            {
                if (waypoint.Length > 0)
                {
                    waypointIndex = Mathf.Min(waypointIndex, waypoint.Length - 1);
                    moveCoroutine = StartCoroutine(MoveTowardsWaypoint());
                }
            }
            else if (currentState == State.chase)
            {
                moveCoroutine = StartCoroutine(MoveTowardsPlayer());
            }
        }

        private IEnumerator MoveTowardsWaypoint()
        {
            if (waypoint.Length == 0) yield break;

            target = waypoint[waypointIndex];
            Debug.Log("Going to waypoint: " + waypointIndex);

            //Monster moving to the waypoint;
            while (Vector3.Distance(transform.position, target.position) > 0.2f)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                yield return null;
            }

            yield return new WaitForSeconds(0.3f);
            waypointIndex++;

            //If we've reached the last waypoint, lthe mosnter disappears;
            if (waypointIndex >= waypoint.Length)
                Destroy(gameObject);

            moveCoroutine = null;
            StartMovement();
        }

        private IEnumerator MoveTowardsPlayer()
        {
            target = player;
            Debug.Log("Chasing player");

            while (Vector3.Distance(transform.position, target.position) > 2f)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.position.x, transform.position.y, player.position.z), speed * 2 * Time.deltaTime);
                yield return null;
            }

            //Capture the player and trigger the event;
            Debug.Log("Captured Player");
            if (!invokeEvent)
            {
                OnPlayerCapture?.Invoke();
                invokeEvent = true;
                Destroy(gameObject, 1f);
            }

            //Stop the coroutine after the player is captured;
            moveCoroutine = null;
        }
    }
}
