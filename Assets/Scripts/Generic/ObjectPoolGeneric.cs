using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolGeneric<T> where T : class
{
    private Dictionary<T, Item<T>> pooledItems = new Dictionary<T, Item<T>>();

    public T GetItemFromPool()
    {
        foreach (var pooledItem in pooledItems)
        {
            if (!pooledItem.Value.IsUsed)
            {
                pooledItem.Value.IsUsed = true;
                return pooledItem.Key;
            }
        }

        T item = CreateAndAddNewItemToPool();

        return item;
    }

    private T CreateAndAddNewItemToPool()
    {
        T newItem = CreateItem();
        Item<T> newItemInfo = new Item<T> { IsUsed = true, ItemType = newItem };
        pooledItems[newItem] = newItemInfo;
        return newItem;
    }

    public void ReturnItem(T itemToReturn)
    {
        if (pooledItems.TryGetValue(itemToReturn, out var pooledItem))
        {
            pooledItem.IsUsed = false;
            Debug.Log("Item returned to the pool: " + itemToReturn);
        }
        else
        {
            Debug.Log("Item is not created yet");
        }
    }

    protected virtual T CreateItem()
    {
        // You can create a new item of the enum type here
        return default(T);
    }

    private class Item<U>
    {
        public bool IsUsed;
        public U ItemType;
    }
}