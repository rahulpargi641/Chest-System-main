using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestPoolService : MonoSingletonGeneric<ChestPoolService>
{
    [SerializeField] private ChestView chestPrefab;

    private Queue<ChestView> objectPool = new Queue<ChestView>();
    private int numberOfSlots;


    private void Start()
    {
        numberOfSlots = SlotService.Instance.GetNumberOfSlots();
        for (int i = 0; i < numberOfSlots; i++)
        {
            ChestView newView = GameObject.Instantiate<ChestView>(chestPrefab);
            objectPool.Enqueue(newView);
            newView.gameObject.SetActive(false);
        }
    }

    public ChestView GetFromPool(ChestController chestController)
    {
        ChestView item = null;
        if (objectPool.Count > 0)
        {
            item = objectPool.Dequeue();
            item.gameObject.SetActive(true);
            item.SetController(chestController);
            item.InitialSettings();
        }
        return item;
    }

    public void ReturnToPool(ChestView item)
    {
        objectPool.Enqueue(item);
        item.gameObject.SetActive(false);
    }
}
