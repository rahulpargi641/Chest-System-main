using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServicePoolGeneric<T, U> : MonoSingletonGeneric<T>
       where T : ServicePoolGeneric<T, U>
       where U : MonoBehaviour
{
    private Queue<U> objectPool;

    private void Awake()
    {
        objectPool = new Queue<U>();
    }

    protected U GetFromPool(Transform newTransform)
    {
        if (objectPool.Count > 0)
        {
            U item = objectPool.Dequeue();
            SetTransform(item, newTransform);
            return item;
        }
        else
            return CreateNewItem(newTransform);
    }

    public void ReturnToPool(U item)
    {
        objectPool.Enqueue(item);
        item.gameObject.SetActive(false);
    }

    protected virtual U CreateNewItem(Transform newTransform)
    {
        return null as U;
    }

    protected void SetTransform(U item, Transform newTransfrom)
    {
        item.transform.position = newTransfrom.position;
        item.transform.rotation = newTransfrom.rotation;
        item.gameObject.SetActive(true);
    }

}
