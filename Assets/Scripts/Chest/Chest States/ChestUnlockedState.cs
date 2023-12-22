using UnityEngine;

public class ChestUnlockedState : IChestState
{
    public EChestState ChestState => EChestState.UNLOCKED;

    private int rewardCoins;
    private int rewardGems;
    private readonly string currentStateName = "Unlocked";
    private readonly string timeLeftUntilUnlock = "Open"; // chest is opened in the unlocked state

    private ChestController controller;

    public ChestUnlockedState(ChestController controller)
    {
        this.controller = controller;
    }

    public void OnEnter()
    {
        UpdateChestImageAndChestInfoTexts();
        GenerateRandomRewards();
    }

    public void OnChestClicked()
    {
        EventService.OnRewardCollected += RemoveChestFromSlot; // When reward message Popup gets closed, this event will be invoked

        SetupChestPopup();
        UIService.Instance.EnableChestPopUp();
        AudioService.Instance.PlaySound(SoundType.RewardsReceived);
    }

    public void OnExit()
    {
        EventService.OnRewardCollected -= RemoveChestFromSlot;

        UIService.Instance.DisableChestPopUp();
    }

    private void UpdateChestImageAndChestInfoTexts()
    {
        controller.UpdateChestImage();
        UpdateChestInfoTexts();
    }

    private void UpdateChestInfoTexts() // Updates Current Chest State and Time remaining until unlock text 
    {
        controller.UpdateCurrentStateText(currentStateName);
        controller.UpdateTimeLeftUntilUnlockText(timeLeftUntilUnlock);
    }

    private void SetupChestPopup()
    {
        CurrencyService.Instance.IncrementCoins(rewardCoins); // Use Events Reward Collected, it should not be in chest popup setup
        CurrencyService.Instance.IncrementGems(rewardGems); // Use Events Reward Collected

        UIService.Instance.UpdateRewardMessageAndEnable(rewardGems, rewardCoins);
    }

    private void GenerateRandomRewards()
    {
        rewardCoins = controller.GenerateRewardCoins();
        rewardGems = controller.GenerateRewardGems();
    }

    private void RemoveChestFromSlot()
    {
        controller.RemoveChestFromSlot();
    }
}