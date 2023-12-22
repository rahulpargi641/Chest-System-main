using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class ChestUnlockingState : IChestState
{
    public EChestState ChestState => EChestState.UNLOCKING;

    private readonly string currentStateName = "Unlocking";
    private int timeLeftUntilUnlock;
    private readonly Vector2 chestPopupCenterPos = new Vector2(0, 0);
    private CancellationTokenSource cancellationTokenSource;

    private readonly ChestController controller;

    public ChestUnlockingState(ChestController controller)
    {
        this.controller = controller;
        timeLeftUntilUnlock = controller.UnlockDurationMinutes * 60;
    }

    public async void OnEnter()
    {
        controller.UpdateCurrentStateText(currentStateName); 

        await StartChestUnlockingTimer();

        controller.UnlockChest();
        AudioService.Instance.PlaySound(SoundType.StartUnlocking);
    }

    public void OnChestClicked()
    {
        SetupChestPopup();
        UIService.Instance.EnableChestPopUp();
        AudioService.Instance.PlaySound(SoundType.ChestClickedOn);
    }

    public void OnExit()
    {
        StopChestUnlockingTimer();
        UIService.Instance.DisableChestPopUp();
        CurrencyService.Instance.DecrementGems(GetGemsToUnlock()); // Use events
        AudioService.Instance.PlaySound(SoundType.Unlocked);
    }

    private async Task StartChestUnlockingTimer()
    {
        cancellationTokenSource = new CancellationTokenSource();
        try
        {
            await ChestUnlockingTimer(cancellationTokenSource.Token);
        }
        catch (TaskCanceledException)
        {
            Debug.Log("Task was canceled");
        }
    }

    private async Task ChestUnlockingTimer(CancellationToken cancellationToken)
    {
        while (timeLeftUntilUnlock >= 0)
        {
            //cancellationToken.ThrowIfCancellationRequested(); -> Check for cancellation before the delay
            if (cancellationToken.IsCancellationRequested) return;

            UpdateTimerText();

            await Task.Delay(1000, cancellationToken);
            timeLeftUntilUnlock--;
        }
    }

    private void UpdateTimerText()
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(timeLeftUntilUnlock);
        string timeString = timeSpan.ToString(@"hh\:mm\:ss");

        controller.UpdateTimeLeftUntilUnlockText(timeString);
    }

    private void StopChestUnlockingTimer()
    {
        cancellationTokenSource?.Cancel();
    }

    private void SetupChestPopup()
    {
        UIService.Instance.SetupAndEnableUnlockNowButton(chestPopupCenterPos, GetGemsToUnlock());
        UIService.Instance.AddButtonsListeners(controller);
    }

    public int GetGemsToUnlock() =>
        Mathf.CeilToInt(timeLeftUntilUnlock / controller.TimeReductionByGemSeconds);
}