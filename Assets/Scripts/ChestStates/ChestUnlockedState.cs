using UnityEngine;
using TMPro;

namespace ChestSystem.Chest
{
    public class ChestUnlockedState : IChestState
    {
        private ChestController chestController;
        private TextMeshProUGUI giftMessage;
        private TextMeshProUGUI giftCoinText;
        private TextMeshProUGUI giftGemText;

        public ChestUnlockedState( ChestController chestController )
        {
            this.chestController = chestController;
            giftMessage = UIService.Instance.GiftMessage;
            giftCoinText = UIService.Instance.GiftCoinText;
            giftGemText = UIService.Instance.GiftGemText;
        }
        public void OnStateEnable( )
        {
            chestController.ChestView.TopText.text = "Unlocked";
            chestController.ChestView.BottomText.text = "OPEN";
            giftMessage.text = "Woooh!!!";
            chestController.ChestView.ChestImage.sprite = chestController.ChestModel.ChestOpenImage;

        }
        public void ChestButtonAction( )
        {
            UIService.OnChestPopUpClosed += DestroyChest;
            giftMessage.gameObject.SetActive( true );
            SetGifts( );
            UIService.Instance.EnableChestPopUp( );
        }
        public void OnStateDisable( )
        {
            UIService.Instance.DisableChestPopUp();
        }
        public ChestState GetChestState( )
        {
            return ChestState.UNLOCKED;
        }
        public int GetRequiredGemsToUnlock( )
        {
            return 0;
        }

        private void SetGifts( )
        {
            int coinsMin = chestController.ChestModel.CoinsMin;
            int coinsMax = chestController.ChestModel.CoinsMax;
            int gemsMin = chestController.ChestModel.GemsMin;
            int gemsMax = chestController.ChestModel.GemsMax;

            int giftCoins = Random.Range( coinsMin, coinsMax + 1 );
            int giftGems = Random.Range( gemsMin, gemsMax + 1 );

            giftCoinText.text = "You got " + giftCoins.ToString( );
            giftGemText.text = "You got " + giftGems.ToString( );

            PlayerService.Instance.IncrementCoins( giftCoins );
            PlayerService.Instance.IncrementGems( giftGems );
        }
        private void DestroyChest( )
        {
            UIService.OnChestPopUpClosed -= DestroyChest;
            OnStateDisable( );
            chestController.ChestView.DestroyChest( );
        }
    }
}