using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChestView : MonoBehaviour
{
    public TextMeshProUGUI CurrentChestStateText => currentChestStateText;
    public TextMeshProUGUI TimeLeftUntilUnlockText => timeLeftUntilUnlockText;
    public Image ChestImage => chestImage;
    public ChestController Controller { private get; set; }
    public EChestState CurrentState => Controller.CurrentState;

    public static event Action<ChestView> OnChestOpened;

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
        chestButton.onClick.AddListener(Controller.ChestButtonClickedOn);
    }

    public void RemoveChest()
    {
        Controller = null;
        chestButton.onClick.RemoveAllListeners();

        OnChestOpened?.Invoke(this); // sends back to pool
    }

    public void StartUnlocking()
    {
        Controller.StartUnlocking();
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