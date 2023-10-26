using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChestService : MonoSingletonGeneric<ChestService>
{
    [SerializeField] private List<ChestRarity> chestList;

    [SerializeField] private Transform chestParentTransform;

    public Transform ChestParentTransform { get { return chestParentTransform; } private set { } }

    private System.Random random = new System.Random(); // Used for generating random numbers.

    /*
     * Select Model according to Probability.
     * Select Controller according to slot available.
     * Get View from object pool.
     * Assign View and Model to the Controller.
     */
    public void SpawnRandomChest()
    {
        ChestSlot slot = SlotService.Instance.GetVacantSlot();
        if (slot == null)
        {
            UIService.Instance.EnableSlotsFullPopUp();
            return;
        }

        ChestRarity chestRarity = GetRandomChestRarity();
        if (chestRarity == null)
        {
            Debug.LogError("No valid chest rarity found.");
            return;
        }

        ChestController controller = slot.GetController();
        controller.SetModel(chestRarity.GetModel());
        controller.SetChestView();
        controller.ChestView.SetSlot(slot);
    }

    private ChestRarity GetRandomChestRarity()
    {
        int totalProbability = chestList.Sum(chest => chest.GetFindingProbability());
        int randomNumber = random.Next(1, totalProbability + 1);

        foreach (var chestRarity in chestList)
        {
            if (randomNumber <= chestRarity.GetFindingProbability())
            {
                return chestRarity;
            }
            else
            {
                randomNumber -= chestRarity.GetFindingProbability();
            }
        }

        // This is a fallback in case something goes wrong.
        return null;
    }
    public bool IsAnyChestUnlocking()
    {
        int numberOfSlots = SlotService.Instance.GetSlotsCount();
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

    private void Start()
    {
        chestList.Sort((p1, p2) => p1.GetFindingProbability().CompareTo(p2.GetFindingProbability()));
        CreateChestModels();
        CreateChestControllers();
    }

    /*
     * Create a chest model for each type of chest (scriptable object). 
     * No of models = No of types of chest
     */
    private void CreateChestModels()
    {
        foreach (var i in chestList)
        {
            ChestModel model = new ChestModel(i.GetChestSO());
            i.SetModel(model);
        }
    }

    /*
     * Create a chest controller for each slot.
     * No of controllers = No of slots
     */
    private void CreateChestControllers()
    {
        ChestScriptableObject chestObject = null;
        int numberOfSlots = SlotService.Instance.GetSlotsCount();
        for (int i = 0; i < numberOfSlots; i++)
        {
            ChestController chestController = new ChestController();
            SlotService.Instance.GetSlotAtPos(i).SetController(chestController);
        }
    }
}