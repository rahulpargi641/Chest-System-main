using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ChestSystem.Chest
{
    public class ChestLockedState : IChestState
    {
        private ChestController chestController;
        private int unlockDurationMinutes;
        private Button unlockNowButton;
        private RectTransform unlockButtonRectTransform;
        private TextMeshProUGUI unlockText;
        private Button setTimerButton;
        private Vector2 unlockButtonInitialPos;
        private Vector2 centerOfChestPopUp = new Vector2( 0, 0 );

        public ChestLockedState( ChestController chestController )
        {
            this.chestController = chestController;
            unlockNowButton = UIService.Instance.UnlockNowButton;
            setTimerButton = UIService.Instance.SetTimerButton;
            unlockButtonRectTransform = UIService.Instance.UnlockNowRectTransform;
            unlockButtonInitialPos = UIService.Instance.UnlockButtonInitialPos;
            unlockText = UIService.Instance.UnlockText;
        }
        public void OnStateEnable( )
        {
            chestController.ChestView.TopText.text = "Locked";
            unlockDurationMinutes = chestController.ChestModel.UnlockDurationMinutes;
            chestController.ChestView.BottomText.text = ( unlockDurationMinutes < 60 ) ? 
                unlockDurationMinutes.ToString( ) + " Min" : ( unlockDurationMinutes / 60 ).ToString( ) + " Hr";
        }
        public void ChestButtonAction( )
        {
            unlockButtonRectTransform.anchoredPosition = unlockButtonInitialPos;
            unlockText.text = "Unlock Now: " + GetRequiredGemsToUnlock( ).ToString( );
            unlockNowButton.gameObject.SetActive( true );

            if ( ChestService.Instance.IsAnyChestUnlocking( ) == false )
                setTimerButton.gameObject.SetActive( true );
            else
                unlockButtonRectTransform.anchoredPosition = centerOfChestPopUp;

            unlockNowButton.onClick.AddListener( chestController.UnlockNow );
            setTimerButton.onClick.AddListener( chestController.StartUnlocking );
            UIService.Instance.EnableChestPopUp( );
        }
        public void OnStateDisable( )
        {
            UIService.Instance.DisableChestPopUp( );
        }
        public ChestState GetChestState( )
        {
            return ChestState.LOCKED;
        }

        public int GetRequiredGemsToUnlock( )
        {
            return Mathf.CeilToInt( unlockDurationMinutes*60 / chestController.TimeSecondsPerGem );
        }
    }
}
