using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIService : MonoSingletonGeneric<UIService>
{
    public static event Action OnRewardCollected;
    public Button StartUnlockingButton => startUnlockingButton;
    public Button UnlockNowButton => unlockNowButton;
    public RectTransform UnlockNowButtonRectTransform => unlockNowButtonRectTransform;
    public TextMeshProUGUI UnlockNowText => unlockNowText;
    public Vector2 UnlockNowButtonInitialPos { get; private set; }
    public TextMeshProUGUI RewardMessage => rewardMessage;
    public TextMeshProUGUI RewardCoinText => rewardCoinText;
    public TextMeshProUGUI RewardGemText => rewardGemText;

    [Header("Raycast Blocker")]
    [SerializeField] private GameObject rayCastBlocker;

    [Header("Buttons")]
    [SerializeField] private Button createChestButton;
    [SerializeField] private Button startUnlockingButton;
    [SerializeField] private Button unlockNowButton;
    [SerializeField] private Button closeChestPopUpButton;
    [SerializeField] private Button closeChestSlotsFullButton;

    [Header("PopUps")]
    [SerializeField] private GameObject chestPopUp;
    [SerializeField] private GameObject chestSlotsFullPopUp;

    [Header("Unlock Now Text")]
    [SerializeField] private RectTransform unlockNowButtonRectTransform;
    [SerializeField] private TextMeshProUGUI unlockNowText;

    [Header("Reward Texts")]
    [SerializeField] private TextMeshProUGUI rewardMessage;
    [SerializeField] private TextMeshProUGUI rewardCoinText;
    [SerializeField] private TextMeshProUGUI rewardGemText;

    [Header("Currency Texts")]
    [SerializeField] private TextMeshProUGUI coins;
    [SerializeField] private TextMeshProUGUI gems;

    private void Start()
    {
        rayCastBlocker.SetActive(false);

        chestPopUp.SetActive(false);
        chestSlotsFullPopUp.SetActive(false);

        unlockNowButton.gameObject.SetActive(false);
        startUnlockingButton.gameObject.SetActive(false);
        UnlockNowButtonInitialPos = unlockNowButtonRectTransform.anchoredPosition;

        rewardMessage.gameObject.SetActive(false);
        SetCurrencyStats();

        createChestButton.onClick.AddListener(CreateChest);
        closeChestSlotsFullButton.onClick.AddListener(DisableSlotsFullPopUp);
        closeChestPopUpButton.onClick.AddListener(DisableChestPopUp);

        AudioService.Instance.PlaySound(SoundType.BgMusic);
    }

    private void CreateChest()
    {
        ChestService.Instance.SpawnRandomChest();
    }

    public void SetCurrencyStats()
    {
        coins.text = CurrencyService.Instance.GetCoinsInAccount().ToString();
        gems.text = CurrencyService.Instance.GetGemsInAccount().ToString();
    }

    public void EnableChestPopUp()
    {
        rayCastBlocker.SetActive(true);
        chestPopUp.SetActive(true);
    }

    public void DisableChestPopUp()
    {
        rayCastBlocker.SetActive(false);
        chestPopUp.SetActive(false);

        unlockNowButton.gameObject.SetActive(false);
        startUnlockingButton.gameObject.SetActive(false);

        rewardMessage.gameObject.SetActive(false);

        OnRewardCollected?.Invoke();

        unlockNowButton.onClick.RemoveAllListeners();
        startUnlockingButton.onClick.RemoveAllListeners();

        AudioService.Instance.PlaySound(SoundType.ButtonClose);
    }

    public void EnableSlotsFullPopUp()
    {
        rayCastBlocker.SetActive(true);
        chestSlotsFullPopUp.SetActive(true);
    }

    private void DisableSlotsFullPopUp()
    {
        rayCastBlocker.SetActive(false);
        chestSlotsFullPopUp.SetActive(false);

        AudioService.Instance.PlaySound(SoundType.ButtonClose);
    }
}