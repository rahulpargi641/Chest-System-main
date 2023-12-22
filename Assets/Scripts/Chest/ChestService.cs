using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ChestService : MonoSingletonGeneric<ChestService>
{
    [SerializeField] List<ChestSO> chestSOs;
    [SerializeField] ChestView chestPrefab;
    [SerializeField] Transform chestParentTransform;

    private ChestPool chestPool = new ChestPool();

    private void Start()
    {
        chestPool.Initialize(chestPrefab);

        chestSOs.Sort((p1, p2) => p1.ChestFindingProbability.CompareTo(p2.ChestFindingProbability)); // arrange ChestSos in ascending order probability

        EventService.OnChestOpened += ReturnChestToPool;
    }

    private void OnDestroy()
    {
        EventService.OnChestOpened -= ReturnChestToPool;
    }

    public void SpawnRandomChest()
    {
        ChestSlot vacantSlot = SlotService.Instance.GetVacantSlot();
        if (vacantSlot == null)
        {
            UIService.Instance.EnableSlotsFullPopUp();
            return;
        }

        ChestSO randomChestSO = SelectChestBasedOnFindingProbability();

        ChestModel chestModel = new ChestModel(randomChestSO);
        ChestView chestView = chestPool.GetChest();
        ChestController chestController = new ChestController(chestModel, chestView);

        chestController.SetupChest(vacantSlot, chestParentTransform);
        chestController.EnableChest();

        SlotService.Instance.AddChestToTheQueue(chestView);

        AudioService.Instance.PlaySound(SoundType.ButtonClick);
    }

    private void ReturnChestToPool(ChestView chestView)
    {
        chestView.DisableChest();
        chestPool.ReturnItem(chestView);
    }

    private ChestSO SelectChestBasedOnFindingProbability()
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