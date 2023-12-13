using UnityEngine;

[System.Serializable]
public class ChestSlot
{
    [field:SerializeField] public RectTransform SlotRectTransform { get; private set; }
    public bool IsEmpty { get; set; } = true;
}
