using UnityEngine;

public class TestMove : MonoBehaviour
{
    private Transform player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, 5f * Time.deltaTime);
    }
}
