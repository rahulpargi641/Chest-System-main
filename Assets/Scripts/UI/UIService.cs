using System;
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
    [SerializeField] private RectTransform unlockNowButtonRectTransform;
    [SerializeField] private TextMeshProUGUI unlockNowText;

    [Header("Reward Texts")]
    [SerializeField] private TextMeshProUGUI rewardMessage;
    [SerializeField] private TextMeshProUGUI rewardCoinText;
    [SerializeField] private TextMeshProUGUI rewardGemText;

    [Header("Currency Texts")]
    [SerializeField] private TextMeshProUGUI coins;
    [SerializeField] private TextMeshProUGUI gems;

    private Vector2 unlockNowButtonInitialPos;

    private void Start()
    {
        rayCastBlocker.SetActive(false);

        chestPopUp.SetActive(false);
        chestSlotsFullPopUp.SetActive(false);

        unlockNowButton.gameObject.SetActive(false);
        startUnlockingButton.gameObject.SetActive(false);

        unlockNowButtonInitialPos = unlockNowButtonRectTransform.anchoredPosition;

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

        EventService.Instance.InvokeOnRewardCollected();

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

    public void SetupAndEnableUnlockNowButton(int gemsToUnlock) // called when chest is in locked state
    {
        unlockNowButtonRectTransform.anchoredPosition = unlockNowButtonInitialPos;
        unlockNowText.text = "Unlock Now: " + gemsToUnlock.ToString();
        unlockNowButton.gameObject.SetActive(true);

    }

    // brings the Unlock Now button to centre of the popup
    public void SetupAndEnableUnlockNowButton(Vector2 newPos, int gemsToUnlock) // called when chest is in unlocking state
    {
        unlockNowButtonRectTransform.anchoredPosition = newPos;
        unlockNowText.text = "Unlock Now: " + gemsToUnlock.ToString();
        unlockNowButton.gameObject.SetActive(true);
    }

    public void EnableStartUnlockingButton()
    {
        startUnlockingButton.gameObject.SetActive(true);
    }

    public void DisableStartUnlockingButton()
    {
        unlockNowButtonRectTransform.anchoredPosition = new Vector2(0,0); // Start Unlocking button doesn't get enabled
    }

    public void AddButtonsListeners(ChestController controller)
    {
        unlockNowButton.onClick.AddListener(controller.UnlockChest);

        if (controller.CurrentState != EChestState.LOCKED) return;

        startUnlockingButton.onClick.AddListener(controller.StartUnlocking);
    }

    public void UpdateRewardMessageAndEnable(int receivedGems, int receivedCoins)
    {
        rewardMessage.text = "Congrats!!";
        rewardGemText.text = "You got  " + receivedGems.ToString();
        rewardCoinText.text = "You got  " + receivedCoins.ToString();

        rewardMessage.gameObject.SetActive(true); // enables the background for the reward texts
    }
}