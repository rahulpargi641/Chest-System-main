using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestView : MonoBehaviour
{
    [SerializeField] private RectTransform chestRectTransform;
    [SerializeField] private Image chestImage;

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

    private void Awake()
    {
        transform.SetParent(ChestService.Instance.ChestParentTransform);
    }

    private void Start()
    {
        chestImage.sprite = chestController.ChestModel.ChestClosedImage;
    }
}
