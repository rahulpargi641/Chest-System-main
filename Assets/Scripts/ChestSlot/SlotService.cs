using System.Collections.Generic;
using UnityEngine;

public class SlotService : MonoSingletonGeneric<SlotService>
{
    [SerializeField] private List<ChestSlot> slotList;

    public ChestSlot GetSlotAtPos(int i)
    {
        return slotList[i];
    }
    public int GetNumberOfSlots()
    {
        return slotList.Count;
    }
    public ChestSlot GetVacantSlot()
    {
        ChestSlot slot = null;
        foreach (var i in slotList)
        {
            if (i.IsSlotEmpty())
            {
                slot = i;
                slot.SetIsEmpty(false);
                break;
            }
        }
        return slot;
    }
}

[System.Serializable]
public class ChestSlot
{
    [SerializeField] private RectTransform slotRectTransform;

    private ChestController chestController;
    private bool isEmpty = true;

    public void SetController(ChestController controller) => chestController = controller;
    public ChestController GetController() => chestController;
    public bool IsSlotEmpty() => isEmpty;
    public void SetIsEmpty(bool value) => isEmpty = value;
    public RectTransform GetRectTransform() => slotRectTransform;
}
