using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestView : MonoBehaviour
{
    [SerializeField] private RectTransform chestRectTransform;

    private ChestController chestController;
    private ChestSlot slot;
    public void SetController(ChestController controller)
    {
        this.chestController = controller;
    }

    //This method requires both chest and slot to be anchored about same anchor.
    public void SetSlot(ChestSlot slot)
    {
        this.slot = slot;
        chestRectTransform.anchoredPosition = slot.GetRectTransform().anchoredPosition;
    }
}