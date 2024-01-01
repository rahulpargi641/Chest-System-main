using UnityEngine;

public class ChestLockedState : IChestState
{
    public EChestState ChestState => EChestState.LOCKED;
    public int GemsToUnlock => gemsToUnlock;

    private const string currentStateName = "Locked";
    private const int minutesPerHour = 60;
    private readonly int unlockDurationMinutes; 
    private readonly int gemsToUnlock;

    private readonly ChestController controller;

    public ChestLockedState(ChestController controller)
    {
        this.controller = controller;

        unlockDurationMinutes = controller.UnlockDurationMinutes;
        gemsToUnlock = Mathf.CeilToInt(unlockDurationMinutes * minutesPerHour / controller.TimeReductionByGemSeconds);
    }

    public void OnEnter()
    {
        UpdateChestImageAndInfoTexts();
    }

    public void OnChestClicked()
    {
        DisplayChestPopup();
        AudioService.Instance.PlaySound(SoundType.ChestClickedOn);
    }

    public void OnExit()
    {
        UIService.Instance.DisableChestPopUp();
    }

    private void UpdateChestImageAndInfoTexts()
    {
        controller.UpdateChestImage();
        UpdateChestInfoTexts();
    }

    private void UpdateChestInfoTexts() // Updates Current Chest State and Time remaining until unlock texts 
    {
        controller.UpdateCurrentStateText(currentStateName);

        string chestUnlockDuration = (unlockDurationMinutes < minutesPerHour) ? unlockDurationMinutes.ToString() + " Min" 
                                                                              : (unlockDurationMinutes / minutesPerHour).ToString() + " Hr";
        controller.UpdateTimeLeftUntilUnlockText(chestUnlockDuration); // Displays time it will take to unlock the chest
    }

    private void DisplayChestPopup()
    {
        SetupChestPopup();
        UIService.Instance.EnableChestPopUp();
    }

    private void SetupChestPopup()
    {
        UIService.Instance.SetupAndEnableUnlockNowButton(gemsToUnlock);
        EnableStartUnlockingButtonIf();
        UIService.Instance.AddButtonsListeners(controller);
    }

    // Start Unlocking button is enabled only if no other chests are currently unlocking
    private void EnableStartUnlockingButtonIf() 
    {
        if (SlotService.Instance.IsAnyChestUnlocking() == false)
            UIService.Instance.EnableStartUnlockingButton();
        else
            UIService.Instance.DisableStartUnlockingButton();
    }
}