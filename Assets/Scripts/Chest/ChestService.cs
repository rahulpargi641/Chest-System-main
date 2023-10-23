using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ChestService : MonoSingletonGeneric<ChestService>
{
    //public ChestController ChestController { get; private set; }

    [SerializeField] private List<ChestRarity> chestList;

    [SerializeField] private Transform chestParentTransform;

    public Transform ChestParentTransform { get { return chestParentTransform; } private set { } }

    private void Start()
    {
        chestList.Sort((p1, p2) => p1.GetProbability().CompareTo(p2.GetProbability()));
        CreateChestModels();
        CreateChestControllers();
    }
    //Make one Chest Model for each scribtable Object.
    private void CreateChestModels()
    {
        foreach (var i in chestList)
        {
            ChestModel model = new ChestModel(i.GetChestObject());
            i.SetModel(model);
        }
    }

    //Make one Chest Controller for each slot
    private void CreateChestControllers()
    {
        ChestScriptableObject chestObject = null;
        int numberOfSlots = SlotService.Instance.GetNumberOfSlots();
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

        int randomNumber = Random.Range(1, 101);
        ChestRarity chestRarity = null;
        int totalProbability = 100;
        foreach (var i in chestList)
        {
            if (randomNumber >= (totalProbability - i.GetProbability()))
            {
                chestRarity = i;
                break;
            }
            else
            {
                totalProbability -= i.GetProbability();
            }
        }
        ChestController controller = slot.GetController();
        controller.SetModel(chestRarity.GetModel());
        controller.SetChestView();
        controller.ChestView.SetSlot(slot);
    }
    public bool IsAnyChestUnlocking()
    {
        int numberOfSlots = SlotService.Instance.GetNumberOfSlots();
        for (int i = 0; i < numberOfSlots; i++)
        {
            ChestSlot slot = SlotService.Instance.GetSlotAtPos(i);
            if (slot.GetController().ChestState == ChestState.UNLOCKING)
            {
                return true;
            }
        }
        return false;
    }

}
[System.Serializable]
public class ChestRarity
{
    [SerializeField] private ChestScriptableObject chestObject;
    [SerializeField] private int probabilityPercentage;

    private ChestModel chestModel;
    public void SetModel(ChestModel model) => chestModel = model;
    public ChestModel GetModel() => chestModel;
    public ChestScriptableObject GetChestObject() => chestObject;
    public int GetProbability() => probabilityPercentage;
}