using System.Collections;
using GlobalSpace;
using UnityEngine;

public class Drops : ObjectPool
{
    [SerializeField] private Transform spawner;
    [SerializeField] AudioSource source;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(StarSpawning());
        }
    }

    IEnumerator StarSpawning()
    {
        while (true)
        {
            GameObject drop = GetPooledObject();
            if (drop != null)
            {
                drop.transform.position = spawner.position;
                drop.SetActive(true);
                source.Play();
            }

            yield return new WaitForSeconds(2f);
            ReturnPooledObject(drop);
        }
    }


}
