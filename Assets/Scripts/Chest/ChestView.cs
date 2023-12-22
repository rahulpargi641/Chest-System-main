using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChestView : MonoBehaviour
{
    public EChestState CurrentState => Controller.CurrentState;
    public ChestController Controller { private get; set; }

    [SerializeField] private RectTransform chestRectTransform;
    [SerializeField] private Image chestImage;
    [SerializeField] private Button chestButton;
    [SerializeField] private TextMeshProUGUI currentChestStateText;
    [SerializeField] private TextMeshProUGUI timeLeftUntilUnlockText;

    public void SetRectTransform(Transform chestParentTransform, Transform slotRectTransform)
    {
        chestRectTransform.position = slotRectTransform.position;
        transform.SetParent(chestParentTransform);
    }

    public void AddChestButtonListener()
    {
        chestButton.onClick.AddListener(Controller.ChestClickedOn);
    }

    public void StartUnlocking()
    {
        Controller.StartUnlocking();
    }

    public void UpdateChestImage(Sprite image)
    {
        chestImage.sprite = image;
    }

    public void UpdateChestStateText(string currentStateName)
    {
        currentChestStateText.text = currentStateName;
    }

    public void UpdateTimeLeftUntilUnlockText(string timeLeftUntilUnlock)
    {
        timeLeftUntilUnlockText.text = timeLeftUntilUnlock;
    }

    public void RemoveChest()
    {
        Controller = null;
        chestButton.onClick.RemoveAllListeners();

        EventService.Instance.InvokeOnChestOpened(this); // sends back to pool
    }

    public void EnableChest()
    {
        gameObject.SetActive(true);
    }

    public void DisableChest()
    {
        gameObject.SetActive(false);
    }
}