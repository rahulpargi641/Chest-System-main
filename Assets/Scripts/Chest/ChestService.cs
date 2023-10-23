using System.Collections.Generic;
using UnityEngine;

public class ChestService : MonoSingletonGeneric<ChestService>
{
    [SerializeField] private List<Chest> chests;
    [SerializeField] private Transform chestParentTransform;
    public Transform ChestParentTransform { get { return chestParentTransform; } private set { } }

    private void Start()
    {
        chests.Sort((p1, p2) => p1.GetFindingProbability().CompareTo(p2.GetFindingProbability()));
        CreateChestModels();
        CreateChestControllers();
    }

    private void CreateChestModels()
    {
        foreach (var chest in chests)
        {
            ChestModel model = new ChestModel(chest.GetChestSO());
            chest.SetModel(model);
        }
    }

    private void CreateChestControllers()
    {
        int numberOfSlots = SlotService.Instance.GetSlotsCount();
        for (int i = 0; i < numberOfSlots; i++)
        {
            ChestController chestController = new ChestController();
            SlotService.Instance.GetSlotAtPos(i).SetController(chestController);
        }
    }
    public void SpawnRandomChest()
    {
        ChestSlot slot = SlotService.Instance.GetVacantSlot();
        if (slot == null)
        {
            UIService.Instance.EnableSlotsFullPopUp();
            return;
        }

        Chest chestToSpawn = GetChestAccordingToFindingProbablity();

        ChestController controller = slot.GetController();
        controller.SetModel(chestToSpawn.GetModel());
        controller.SetChestView();
        controller.ChestView.SetSlot(slot);
    }

    private Chest GetChestAccordingToFindingProbablity()
    {
        Chest chestToSpawn = null;

        int randomNumber = Random.Range(1, 101);
        int totalProbability = 100;

        foreach (var chest in chests)
        {
            if (randomNumber >= (totalProbability - chest.GetFindingProbability()))
                chestToSpawn = chest;
            else
                totalProbability -= chest.GetFindingProbability();
        }

        return chestToSpawn;
    }

    public bool IsAnyChestUnlocking()
    {
        int nSlots = SlotService.Instance.GetSlotsCount();

        for (int i = 0; i < nSlots; i++)
        {
            ChestSlot slot = SlotService.Instance.GetSlotAtPos(i);
            if (slot.GetController().ChestState == ChestState.UNLOCKING)
                return true;
        }
        return false;
    }
}