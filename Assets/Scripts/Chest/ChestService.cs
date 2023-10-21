using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestService : MonoSingletonGeneric<ChestService>
{
    public ChestController ChestController { get; private set; }

    [SerializeField] private List<ChestRarity> chestList;
    [SerializeField] private ChestView chestPrefab;
    [SerializeField] private Transform chestParentTransform;

    private ChestModel chestModel;
    private ChestView chestView;

    public Transform ChestParentTransform { get { return chestParentTransform; } private set { } }

    private void Start()
    {
        chestList.Sort((p1, p2) => p1.GetProbability().CompareTo(p2.GetProbability()));
    }
    public void CreateRandomChest()
    {
        ChestSlot slot = SlotService.Instance.GetVacantSlot();
        if (slot == null)
        {
            UIService.Instance.EnableSlotsFullPopUp();
            return;
        }

        int randomNumber = Random.Range(1, 101);
        ChestScriptableObject chestObject = null;

        foreach (var i in chestList)
        {
            if (randomNumber >= i.GetProbability())
            {
                chestObject = i.GetChestObject();
            }
        }

        chestModel = new ChestModel(chestObject);
        ChestController = new ChestController(chestModel, chestPrefab);
        chestView = ChestController.ChestView;
        chestView.SetSlot(slot);
    }

}
[System.Serializable]
public class ChestRarity
{
    [SerializeField] private ChestScriptableObject chestObject;
    [SerializeField] private int probabilityPercentage;

    public ChestScriptableObject GetChestObject() => chestObject;
    public int GetProbability() => probabilityPercentage;
}
