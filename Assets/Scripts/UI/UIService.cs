using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIService : MonoSingletonGeneric<UIService>
{
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
    [SerializeField] private TextMeshProUGUI unlockNowText;
    [SerializeField] private RectTransform unlockNowButtonRectTransform;

    [Header("Currency Texts")]
    [SerializeField] private TextMeshProUGUI coins;
    [SerializeField] private TextMeshProUGUI gems;

    [Header("Reward Texts")]
    [SerializeField] private TextMeshProUGUI rewardMessage;
    [SerializeField] private TextMeshProUGUI rewardCoinText;
    [SerializeField] private TextMeshProUGUI rewardGemText;


    private Vector2 unlockNowButtonInitialPos;

    private void Start()
    {
        InitializeUI();
        SetupButtonListeners();
        SubscribeToEvents();
        PlayBackgroundMusic();
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }

    private void InitializeUI()
    {
        rayCastBlocker.SetActive(false);
        chestPopUp.SetActive(false);
        chestSlotsFullPopUp.SetActive(false);
        unlockNowButton.gameObject.SetActive(false);
        startUnlockingButton.gameObject.SetActive(false);
        rewardMessage.gameObject.SetActive(false);

        unlockNowButtonInitialPos = unlockNowButtonRectTransform.anchoredPosition;
        UpdateCurrencyStats();
    }

    private void SetupButtonListeners()
    {
        createChestButton.onClick.AddListener(CreateChest);
        closeChestSlotsFullButton.onClick.AddListener(DisableSlotsFullPopUp);
        closeChestPopUpButton.onClick.AddListener(DisableChestPopUp);
    }

    private void SubscribeToEvents()
    {
        EventService.onChestSlotsFull += EnableSlotsFullPopUp;

        EventService.onGemsUsed += UpdateGemsStats;
        EventService.onRewardCollected += UpdateCurrencyStats;
    }

    private void UnsubscribeFromEvents()
    {
        EventService.onChestSlotsFull -= EnableSlotsFullPopUp;

        EventService.onGemsUsed -= UpdateGemsStats;
        EventService.onRewardCollected -= UpdateCurrencyStats;
    }

    private void PlayBackgroundMusic()
    {
        AudioService.Instance.PlaySound(SoundType.BgMusic);
    }

    private void CreateChest()
    {
        ChestService.Instance.SpawnRandomChest();
    }

    private void UpdateCurrencyStats()
    {
        gems.text = PlayerCurrencyService.Instance.GemsInAccount.ToString();
        coins.text = PlayerCurrencyService.Instance.CoinsInAccount.ToString();
    }

    private void UpdateCurrencyStats(int gems, int coins)
    {
        UpdateGemsStats(gems);
        UpdateCoinsStats(coins);
    }

    private void UpdateGemsStats(int gems)
    {
        this.gems.text = gems.ToString();
    }

    private void UpdateCoinsStats(int coins)
    {
        this.coins.text = coins.ToString();
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

        unlockNowButton.onClick.RemoveAllListeners();
        startUnlockingButton.onClick.RemoveAllListeners();

        EventService.Instance.InvokeOnRewardCollected();
        AudioService.Instance.PlaySound(SoundType.ButtonClose);
    }

    private void EnableSlotsFullPopUp()
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

    // called when chest is in Locked state, UnlockNow button will be at the original position
    public void SetupAndEnableUnlockNowButton(int gemsToUnlock) 
    {
        SetupUnlockNowButton(unlockNowButtonInitialPos, gemsToUnlock);
        unlockNowButton.gameObject.SetActive(true);
    }

    // called when chest is in Unlocking state, UnlockNow button will be at the center of chest popup
    public void SetupAndEnableUnlockNowButton(Vector2 chestPopupCenterPos, int gemsToUnlock) 
    {
        SetupUnlockNowButton(chestPopupCenterPos, gemsToUnlock);
        unlockNowButton.gameObject.SetActive(true);
    }

    private void SetupUnlockNowButton(Vector2 newPos, int gemsToUnlock) 
    {
        unlockNowButtonRectTransform.anchoredPosition = newPos;
        unlockNowText.text = $"Unlock Now: {gemsToUnlock}";
    }

    public void EnableStartUnlockingButton()
    {
        startUnlockingButton.gameObject.SetActive(true);
    }

    public void DisableStartUnlockingButton()
    {
        unlockNowButtonRectTransform.anchoredPosition = Vector2.zero;
    }

    public void AddButtonsListeners(ChestController controller)
    {
        unlockNowButton.onClick.AddListener(controller.UnlockChestNow);

        if (controller.CurrentState == EChestState.LOCKED)
            startUnlockingButton.onClick.AddListener(controller.StartUnlocking);
    }

    public void UpdateRewardMessageAndEnable(int receivedGems, int receivedCoins)
    {
        rewardMessage.text = "Congrats!!";
        rewardGemText.text = $"You got  {receivedGems}";
        rewardCoinText.text = $"You got  {receivedCoins}";

        rewardMessage.gameObject.SetActive(true);
    }
}
