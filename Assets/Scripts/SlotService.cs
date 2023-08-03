using UnityEngine;
using System.Collections.Generic;

namespace ChestSystem.Chest
{
    public class SlotService : MonoSingletonGeneric<SlotService>
    {
        [SerializeField] private List<ChestSlot> slotList;

        public ChestSlot GetSlotAtPos(int i )
        {
            return slotList[i];
        }
        public int GetNumberOfSlots( )
        {
            return slotList.Count;
        }
        public ChestSlot GetVacantSlot( )
        {
            ChestSlot slot = null;
            foreach(var i in slotList )
            {
                if ( i.IsSlotEmpty( ) )
                {
                    slot = i;
                    slot.SetIsEmpty( false );
                    break;
                }
            }
            return slot;
        }
    }    
}
