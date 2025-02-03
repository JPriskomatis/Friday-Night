using System.Collections;
using System.Collections.Generic;
using GlobalSpace;
using UnityEngine;
using UnityEngine.Pool;

namespace GlobalSpace
{

    public abstract class ObjectPool : Singleton<ObjectPool>
    {
        [SerializeField] protected int poolSize;
        [SerializeField] protected GameObject pooledObject;
        protected Queue<GameObject> pooledObjects;

        private void Start()
        {
            pooledObjects = new Queue<GameObject>();

            //This tmp value will be the prefab to populate our Queue
            GameObject tmp;

            for (int i = 0; i < poolSize; i++)
            {
                //We populate the Queue with our prefab;
                tmp = Instantiate(pooledObject);

                //Important to set it as inactive;
                tmp.SetActive(false);

                //Enqueue adds an element to the Queue;
                pooledObjects.Enqueue(tmp);

            }
        }

        public GameObject GetPooledObject()
        {
            if (pooledObjects.Count > 0)
            {
                //Removes an item from the Queue;
                GameObject obj = pooledObjects.Dequeue();

                if (obj != null)
                {

                    //This is the gameobject that we activate in our scene;
                    obj.SetActive(true);
                    return obj;
                }
            }
            return null;
        }

        public void ReturnPooledObject(GameObject obj)
        {
            obj.SetActive(false);

            //Endqueue adds the item to the end of our queue;
            pooledObjects.Enqueue(obj);
        }
    }


}