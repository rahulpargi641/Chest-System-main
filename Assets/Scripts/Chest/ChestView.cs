using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChestView : MonoBehaviour
{
    public Button ChestButton { get { return chestButton; } private set { } }
    public TextMeshProUGUI TopText { get { return topText; } private set { } }
    public TextMeshProUGUI BottomText { get { return bottomText; } private set { } }

    [SerializeField] private RectTransform chestRectTransform;
    [SerializeField] private Image chestImage;
    [SerializeField] private Button chestButton;
    [SerializeField] private TextMeshProUGUI topText;
    [SerializeField] private TextMeshProUGUI bottomText;

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
    public void ChangeChestImage()
    {
        chestImage.sprite = chestController.ChestModel.ChestClosedImage;
    }
    public void DestroyChest()
    {
        slot.SetIsEmpty(true);
        Destroy(this.gameObject);
    }

    private void Awake()
    {
        transform.SetParent(ChestService.Instance.ChestParentTransform);
    }

    private void Start()
    {
        ChangeChestImage();

        chestButton.onClick.AddListener(chestController.ChestButtonAction);
    }
}