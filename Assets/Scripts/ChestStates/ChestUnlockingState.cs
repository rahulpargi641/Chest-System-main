using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ChestSystem.Chest
{
    public class ChestUnlockingState : IChestState
    {
        private ChestController chestController;

        private Button unlockNowButton;
        private RectTransform unlockButtonRectTransform;
        private TextMeshProUGUI unlockText;
        private Vector2 centerOfChestPopUp = new Vector2( 0, 0 );
        private Coroutine countDown;

        public ChestUnlockingState( ChestController chestController )
        {
            this.chestController = chestController;
            unlockNowButton = UIService.Instance.UnlockNowButton;
            unlockButtonRectTransform = UIService.Instance.UnlockNowRectTransform;
            unlockText = UIService.Instance.UnlockText;
            
        }
        public void OnStateEnable( )
        {
            chestController.ChestView.TopText.text = "Unlocking";
            countDown = chestController.ChestView.StartCoroutine( chestController.ChestView.CountDown() );
        }
        public void ChestButtonAction( )
        {
            //bring the Unlock button to centre of the popup
            unlockButtonRectTransform.anchoredPosition = centerOfChestPopUp;

            unlockNowButton.gameObject.SetActive( true );
            unlockText.text = "Unlock Now: " + GetRequiredGemsToUnlock( ).ToString( );
            unlockNowButton.onClick.AddListener( chestController.UnlockNow );
            UIService.Instance.EnableChestPopUp( );
        }
        public void OnStateDisable( )
        {
            UIService.Instance.DisableChestPopUp( );

            chestController.ChestView.StopCoroutine( countDown );
        }
        public ChestState GetChestState( )
        {
            return ChestState.UNLOCKING;
        }

        public int GetRequiredGemsToUnlock( )
        {
            return Mathf.CeilToInt( chestController.ChestView.TimeRemainingSeconds / chestController.TimeSecondsPerGem );
        }
    }
}