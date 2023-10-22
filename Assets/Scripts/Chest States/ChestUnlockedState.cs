using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChestUnlockedState : IChestState
{
    private ChestController chestController;
    private TextMeshProUGUI giftMessage;
    private TextMeshProUGUI giftCoinText;
    private TextMeshProUGUI giftGemText;

    public ChestUnlockedState(ChestController chestController)
    {
        this.chestController = chestController;
        giftMessage = UIService.Instance.GiftMessage;
        giftCoinText = UIService.Instance.GiftCoinText;
        giftGemText = UIService.Instance.GiftGemText;
    }
    public void OnStateEnable()
    {
        chestController.ChestView.TopText.text = "Unlocked";
        chestController.ChestView.BottomText.text = "OPEN";
        giftMessage.text = "Woooh!!!";

        giftMessage.gameObject.SetActive(true);
        giftCoinText.gameObject.SetActive(true);
        giftGemText.gameObject.SetActive(true);
        SetGifts();

        UIService.OnChestPopUpClosed += this.OnStateDisable;
    }
    private void SetGifts()
    {
        int coinsMin = chestController.ChestModel.CoinsMin;
        int coinsMax = chestController.ChestModel.CoinsMax;
        int gemsMin = chestController.ChestModel.GemsMin;
        int gemsMax = chestController.ChestModel.GemsMax;

        int giftCoins = Random.Range(coinsMin, coinsMax + 1);
        int giftGems = Random.Range(gemsMin, gemsMax + 1);

        giftCoinText.text = "You got " + giftCoins.ToString();
        giftGemText.text = "You got " + giftGems.ToString();
    }
    public void ChestButtonAction()
    {
        UIService.Instance.EnableChestPopUp();
    }
    public void OnStateDisable()
    {
        giftMessage.gameObject.SetActive(false);
        giftCoinText.gameObject.SetActive(false);
        giftGemText.gameObject.SetActive(false);

        chestController.ChestView.DestroyChest();
    }
    public ChestState GetChestState()
    {
        return ChestState.UNLOCKED;
    }
    public int GetRequiredGemsToUnlock()
    {
        return 0;
    }
}