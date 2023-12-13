using TMPro;
using UnityEngine;

public class ChestUnlockedState : IChestState
{
    private ChestController controller;

    private TextMeshProUGUI rewardMessage;
    private TextMeshProUGUI rewardCoinText;
    private TextMeshProUGUI rewardGemText;

    public ChestUnlockedState(ChestController controller)
    {
        this.controller = controller;

        rewardMessage = UIService.Instance.RewardMessage;
        rewardCoinText = UIService.Instance.RewardCoinText;
        rewardGemText = UIService.Instance.RewardGemText;
    }

    public void OnStateEnter()
    {
        UpdateChestVisualAndChestInfoTexts();

        CurrencyService.Instance.DecrementGems(GetRequiredGemsToUnlock());

        AudioService.Instance.PlaySound(SoundType.Unlocked);
    }

    private void UpdateChestVisualAndChestInfoTexts()
    {
        controller.View.ChestImage.sprite = controller.Model.ChestOpenImage; // updates chest visual
        UpdateChestInfoTexts();
    }

    private void UpdateChestInfoTexts() // Updates Current Chest State and Time remaining until unlock text 
    {
        controller.View.CurrentChestStateText.text = "Unlocked";
        controller.View.TimeLeftUntilUnlockText.text = "OPEN";
    }

    // get called when Chest is clicked on 
    public void ChestButtonClickedOn()
    {
        UIService.OnRewardCollected += RemoveChestFromSlot; // When reward message Popup gets closed, this event will be invoked

        ChestPopSetup();
        UIService.Instance.EnableChestPopUp();

        AudioService.Instance.PlaySound(SoundType.RewardsReceived);
    }

    private void ChestPopSetup()
    {
        rewardMessage.gameObject.SetActive(true); // enables the background for the reward texts
        SetRewardsTexts();
    }

    public void OnStateExit()
    {
        UIService.OnRewardCollected -= RemoveChestFromSlot;

        UIService.Instance.DisableChestPopUp();
    }

  
    private void SetRewardsTexts()
    {
        int coinsMin = controller.Model.CoinsMin;
        int coinsMax = controller.Model.CoinsMax;
        int gemsMin = controller.Model.GemsMin;
        int gemsMax = controller.Model.GemsMax;

        int rewardCoins = Random.Range(coinsMin, coinsMax + 1);
        int rewardGems = Random.Range(gemsMin, gemsMax + 1);

        rewardMessage.text = "Congrats!!";
        rewardCoinText.text = "You got  " + rewardCoins.ToString();
        rewardGemText.text = "You got  " + rewardGems.ToString();

        CurrencyService.Instance.IncrementCoins(rewardCoins);
        CurrencyService.Instance.IncrementGems(rewardGems);
    }

    private void RemoveChestFromSlot()
    {
        controller.RemoveChestFromSlot();
    }

    public int GetRequiredGemsToUnlock() => 
        Mathf.CeilToInt(controller.Model.UnlockDurationMinutes * 60 / controller.Model.TimeReductionByGemSeconds);


    public EChestState GetChestState() => EChestState.UNLOCKED;
}