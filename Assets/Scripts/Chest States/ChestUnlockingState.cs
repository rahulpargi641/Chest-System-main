using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestUnlockingState : IChestState
{
    private ChestController chestController;
    private int timeRemainingSeconds;

    private Button unlockNowButton;
    private RectTransform unlockButtonRectTransform;
    private Vector2 centerOfChestPopUp = new Vector2(0, 0);
    private Vector2 unlockButtonInitialPos;

    public ChestUnlockingState(ChestController chestController)
    {
        this.chestController = chestController;
        unlockNowButton = UIService.Instance.UnlockNowButton;
        unlockButtonRectTransform = UIService.Instance.UnlockNowRectTransform;
        unlockButtonInitialPos = unlockButtonRectTransform.anchoredPosition;

        unlockNowButton.onClick.AddListener(chestController.UnlockNow);
    }
    public void OnStateEnable()
    {
        chestController.ChestView.TopText.text = "Unlocking";
        timeRemainingSeconds = chestController.ChestModel.UnlockDurationMinutes * 60;

        //bring the Unlock button to centre of the popup
        unlockButtonRectTransform.anchoredPosition = centerOfChestPopUp;
        unlockNowButton.gameObject.SetActive(true);

        CountDown();
    }
    public void ChestButtonAction()
    {
        UIService.Instance.EnableChestPopUp();
    }
    public void OnStateDisable()
    {
        unlockButtonRectTransform.anchoredPosition = unlockButtonInitialPos;
        unlockNowButton.gameObject.SetActive(false);
        UIService.Instance.DisableChestPopUp();
    }
    public ChestState GetChestState()
    {
        return ChestState.UNLOCKING;
    }
    private async void CountDown()
    {
        while (timeRemainingSeconds >= 0)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(timeRemainingSeconds);
            string timeString = timeSpan.ToString(@"hh\:mm\:ss");
            chestController.ChestView.BottomText.text = timeString;

            timeRemainingSeconds--;

            await Task.Delay(1000);
        }
        chestController.UnlockNow();
    }

    public int GetRequiredGemsToUnlock()
    {
        return Mathf.CeilToInt(timeRemainingSeconds / chestController.TimeSecondsPerGem);
    }
}
