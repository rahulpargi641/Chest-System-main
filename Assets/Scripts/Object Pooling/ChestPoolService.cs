using System.Collections.Generic;
using UnityEngine;

public class ChestPoolService : MonoSingletonGeneric<ChestPoolService>
{
    [SerializeField] private ChestView chestPrefab;

    private Queue<ChestView> chestPool = new Queue<ChestView>();
    private int numberOfSlots;

    private void Start()
    {
        CreateChestViews();
    }

    private void CreateChestViews()
    {
        numberOfSlots = SlotService.Instance.GetSlotsCount();
        for (int i = 0; i < numberOfSlots; i++)
        {
            ChestView spawnedChest = Instantiate(chestPrefab);
            chestPool.Enqueue(spawnedChest);
            spawnedChest.gameObject.SetActive(false);
        }
    }

    public ChestView GetFromPool(ChestController chestController)
    {
        ChestView item = null;
        if (chestPool.Count > 0)
        {
            item = chestPool.Dequeue();
            item.gameObject.SetActive(true);
            item.SetController(chestController);
            item.InitialSettings();
        }
        return item;
    }

    public void ReturnToPool(ChestView item)
    {
        chestPool.Enqueue(item);
        item.gameObject.SetActive(false);
    }
}
