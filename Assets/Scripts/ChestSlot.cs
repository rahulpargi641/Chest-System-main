using UnityEngine;

namespace ChestSystem.Chest
{
    [System.Serializable]
    public class ChestSlot
    {
        [SerializeField] private RectTransform slotRectTransform;

        private ChestController chestController;
        private bool isEmpty = true;

        public void SetController( ChestController controller ) => chestController = controller;
        public ChestController GetController( ) => chestController;
        public bool IsSlotEmpty( ) => isEmpty;
        public void SetIsEmpty( bool value ) => isEmpty = value;
        public RectTransform GetRectTransform( ) => slotRectTransform;
    }
}
