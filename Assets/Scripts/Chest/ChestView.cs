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
    public Image ChestImage { get { return chestImage; } private set { } }

    public int TimeRemainingSeconds { get; private set; }

    [SerializeField] private RectTransform chestRectTransform;
    [SerializeField] private Image chestImage;
    [SerializeField] private Button chestButton;
    [SerializeField] private TextMeshProUGUI topText;
    [SerializeField] private TextMeshProUGUI bottomText;

    private ChestController chestController;
    private ChestSlot slot;
    private Coroutine countDown;
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
        ChestService.Instance.ChestControllerList.Remove(this.chestController);
        Destroy(this.gameObject);
    }

    public IEnumerator CountDown()
    {
        while (TimeRemainingSeconds >= 0)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(TimeRemainingSeconds);
            string timeString = timeSpan.ToString(@"hh\:mm\:ss");
            BottomText.text = timeString;

            TimeRemainingSeconds--;
            yield return new WaitForSeconds(1);
        }
        chestController.UnlockNow();
    }

    private void Awake()
    {
        transform.SetParent(ChestService.Instance.ChestParentTransform);
        chestRectTransform.localScale = new Vector3(1, 1, 1);
    }

    private void Start()
    {
        ChangeChestImage();

        chestButton.onClick.AddListener(chestController.ChestButtonAction);

        TimeRemainingSeconds = chestController.ChestModel.UnlockDurationMinutes * 60;
    }
}