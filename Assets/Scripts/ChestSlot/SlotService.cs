using System.Collections.Generic;
using UnityEngine;

public class SlotService : MonoSingletonGeneric<SlotService>
{
    [SerializeField] private List<ChestSlot> slots;

    public ChestSlot GetSlotAtPos(int i)
    {
        return slots[i];
    }

    public int GetSlotsCount()
    {
        return slots.Count;
    }

    public ChestSlot GetVacantSlot()
    {
        ChestSlot vacantSlot = null;
        foreach (var slot in slots)
        {
            if (slot.IsSlotEmpty())
            {
                vacantSlot = slot;
                vacantSlot.SetIsEmpty(false);
                break;
            }
        }
        return vacantSlot;
    }
}

