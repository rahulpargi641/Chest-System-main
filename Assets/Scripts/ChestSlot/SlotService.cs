using System.Collections.Generic;
using UnityEngine;

public class SlotService : MonoSingletonGeneric<SlotService>
{
    [SerializeField] private List<ChestSlot> slotList;

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
    private bool isEmpty = true;

    public bool IsSlotEmpty() => isEmpty;
    public void SetIsEmpty(bool value) => isEmpty = value;
    public RectTransform GetRectTransform() => slotRectTransform;
}