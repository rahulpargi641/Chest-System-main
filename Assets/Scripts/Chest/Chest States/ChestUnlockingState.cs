using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class ChestUnlockingState : IChestState
{
    public EChestState ChestState => EChestState.UNLOCKING;
    public int GemsToUnlock => Mathf.CeilToInt(timeLeftUntilUnlock / controller.TimeReductionByGemSeconds);

    private const string currentStateName = "Unlocking";
    private const int minutesPerHour = 60;
    private readonly Vector2 chestPopupCenterPos = new Vector2(0, 0);
    private int timeLeftUntilUnlock;
    private CancellationTokenSource cancellationTokenSource;

    private readonly ChestController controller;

    public ChestUnlockingState(ChestController controller)
    {
        this.controller = controller;
        timeLeftUntilUnlock = controller.UnlockDurationMinutes * minutesPerHour;
    }

    public async void OnEnter()
    {
        controller.UpdateCurrentStateText(currentStateName); 

        await StartChestUnlockingTimer();

        controller.UnlockChestNow();
        AudioService.Instance.PlaySound(SoundType.StartUnlocking);
    }

    public void OnChestClicked()
    {
        DisplayChestPopup();
        AudioService.Instance.PlaySound(SoundType.ChestClickedOn);
    }

    public void OnExit()
    {
        StopChestUnlockingTimer();
        UIService.Instance.DisableChestPopUp();
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

    private void DisplayChestPopup()
    {
        SetupChestPopup();
        UIService.Instance.EnableChestPopUp();
    }

    private void SetupChestPopup()
    {
        UIService.Instance.SetupAndEnableUnlockNowButton(chestPopupCenterPos, GemsToUnlock);
        UIService.Instance.AddButtonsListeners(controller);
    }
}