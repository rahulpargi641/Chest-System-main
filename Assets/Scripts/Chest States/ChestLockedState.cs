using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestLockedState : IChestState
{
    private ChestController chestController;
    private int unlockDurationMinutes;
    private Button unlockNowButton;
    private Button setTimerButton;

    public ChestLockedState(ChestController chestController)
    {
        this.chestController = chestController;
        unlockNowButton = UIService.Instance.UnlockNowButton;
        setTimerButton = UIService.Instance.SetTimerButton;

        unlockNowButton.onClick.AddListener(chestController.UnlockNow);
        setTimerButton.onClick.AddListener(chestController.StartUnlocking);
    }
    public void OnStateEnable()
    {
        chestController.ChestView.TopText.text = "Locked";
        unlockDurationMinutes = chestController.ChestModel.UnlockDurationMinutes;
        chestController.ChestView.BottomText.text = (unlockDurationMinutes < 60) ?
            unlockDurationMinutes.ToString() + " Min" : (unlockDurationMinutes / 60).ToString() + " Hr";

        unlockNowButton.gameObject.SetActive(true);
        setTimerButton.gameObject.SetActive(true);
    }
    public void ChestButtonAction()
    {
        UIService.Instance.EnableChestPopUp();
    }
    public void OnStateDisable()
    {
        unlockNowButton.gameObject.SetActive(false);
        setTimerButton.gameObject.SetActive(false);
        UIService.Instance.DisableChestPopUp();
    }
    public ChestState GetChestState()
    {
        return ChestState.LOCKED;
    }

    public int GetRequiredGemsToUnlock()
    {
        return Mathf.CeilToInt(unlockDurationMinutes * 60 / chestController.TimeSecondsPerGem);
    }
}