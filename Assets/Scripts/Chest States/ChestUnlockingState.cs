using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChestUnlockingState : IChestState
{
    private ChestController chestController;
    private int timeRemainingSeconds;

    private Button unlockNowButton;
    private RectTransform unlockButtonRectTransform;
    private TextMeshProUGUI unlockText;
    private Vector2 centerOfChestPopUp = new Vector2(0, 0);

    private CancellationTokenSource cancellationTokenSource;

    public ChestUnlockingState(ChestController chestController)
    {
        this.chestController = chestController;
        unlockNowButton = UIService.Instance.UnlockNowButton;
        unlockButtonRectTransform = UIService.Instance.UnlockNowRectTransform;
        unlockText = UIService.Instance.UnlockText;
    }
    public void OnStateEnable()
    {
        chestController.ChestView.TopText.text = "Unlocking";
        timeRemainingSeconds = chestController.ChestModel.UnlockDurationMinutes * 60;
        CountDown();
    }
    public void ChestButtonAction()
    {
        //bring the Unlock button to centre of the popup
        unlockButtonRectTransform.anchoredPosition = centerOfChestPopUp;
        unlockNowButton.gameObject.SetActive(true);
        unlockText.text = "Unlock Now: " + GetRequiredGemsToUnlock().ToString();
        unlockNowButton.onClick.AddListener(chestController.UnlockNow);
        UIService.Instance.EnableChestPopUp();
    }
    public void OnStateDisable()
    {
        UIService.Instance.DisableChestPopUp();

        cancellationTokenSource?.Cancel();
    }
    public ChestState GetChestState()
    {
        return ChestState.UNLOCKING;
    }
    private async void CountDown()
    {
        cancellationTokenSource = new CancellationTokenSource();

        while (timeRemainingSeconds >= 0)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(timeRemainingSeconds);
            string timeString = timeSpan.ToString(@"hh\:mm\:ss");
            chestController.ChestView.BottomText.text = timeString;

            timeRemainingSeconds--;
            try
            {
                await Task.Delay(1000, cancellationTokenSource.Token);
            }
            catch
            {
                cancellationTokenSource?.Dispose();
                cancellationTokenSource = null;
                return;
            }
        }
        chestController.UnlockNow();
    }

    public int GetRequiredGemsToUnlock()
    {
        return Mathf.CeilToInt(timeRemainingSeconds / chestController.TimeSecondsPerGem);
    }
}
