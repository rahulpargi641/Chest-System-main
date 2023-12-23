using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChestService : MonoSingletonGeneric<ChestService>
{
    [SerializeField] private List<ChestSO> chestSOs;
    [SerializeField] private ChestView chestPrefab;
    [SerializeField] private Transform chestParentTransform;

    private ChestPool chestPool;

    private void Start()
    {
        InitializeChestPool();
        SortChestSOsByProbability();
        SubscribeToEvents();
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }

    private void InitializeChestPool()
    {
        chestPool = new ChestPool();
        chestPool.Initialize(chestPrefab);
    }

    private void SortChestSOsByProbability()
    {
        chestSOs.Sort((p1, p2) => p1.ChestFindingProbability.CompareTo(p2.ChestFindingProbability));
    }

    private void SubscribeToEvents()
    {
        EventService.OnChestOpened += ReturnChestToPool;
    }

    private void UnsubscribeFromEvents()
    {
        EventService.OnChestOpened -= ReturnChestToPool;
    }

    public void SpawnRandomChest()
    {
        ChestSlot vacantSlot = SlotService.Instance.GetVacantSlot();
        if (vacantSlot == null)
        {
            EventService.Instance.InvokeOnChestSlotsFull();
            return;
        }
        
        SpawnRandomChest(vacantSlot);

        AudioService.Instance.PlaySound(SoundType.ButtonClick);
    }

    private void SpawnRandomChest(ChestSlot vacantSlot)
    {
        ChestSO randomChestSO = GetRandomChestSO();
        ChestView chestView = SpawnChest(vacantSlot, randomChestSO);
        SlotService.Instance.AddChestToTheQueue(chestView);
    }

    private ChestView SpawnChest(ChestSlot vacantSlot, ChestSO randomChestSO)
    {
        ChestModel chestModel = new ChestModel(randomChestSO);
        ChestView chestView = chestPool.GetChest();
        ChestController chestController = new ChestController(chestModel, chestView);

        chestController.SetupChest(vacantSlot, chestParentTransform);
        chestController.EnableChest();
        return chestView;
    }

    private void ReturnChestToPool(ChestView chestView)
    {
        chestView.DisableChest();
        chestPool.ReturnItem(chestView);
    }

    // Selects chest according to ChestFindingProbability or rarity
    private ChestSO GetRandomChestSO()
    {
        System.Random random = new System.Random();

        int totalProbability = chestSOs.Sum(chest => chest.ChestFindingProbability);
        int randomNumber = random.Next(1, totalProbability + 1);

        foreach (ChestSO chestSO in chestSOs)
        {
            if (randomNumber <= chestSO.ChestFindingProbability)
                return chestSO;
            else
                randomNumber -= chestSO.ChestFindingProbability;
        }

        // This is a fallback in case something goes wrong.
        return null;
    }
}
