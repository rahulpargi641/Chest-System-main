using System;
using UnityEngine;

public class ChestController
{
    public int UnlockDurationMinutes => model.UnlockDurationMinutes;
    public float TimeReductionByGemSeconds => model.TimeReductionByGemSeconds;
    public EChestState CurrentState => currentState.ChestState;

    private ChestLockedState chestLocked;
    private ChestUnlockingState chestUnlocking;
    private ChestUnlockedState chestUnlocked;
    private IChestState currentState;

    private ChestSlot chestSlot;

    private readonly ChestModel model;
    private ChestView view;

    public ChestController(ChestModel model, ChestView view)
    {
        this.model = model;
        this.view = view;
        view.Controller = this;

        CreateChestStates();
        SetCurrentState();
    }

    private void CreateChestStates()
    {
        chestLocked = new ChestLockedState(this);
        chestUnlocking = new ChestUnlockingState(this);
        chestUnlocked = new ChestUnlockedState(this);
    }

    private void SetCurrentState()
    {
        currentState = chestLocked;
        currentState.OnEnter();
    }

    public void SetupChest(ChestSlot chestSlot, Transform chestParentTransform)
    {
        this.chestSlot = chestSlot;

        view.SetRectTransform(chestParentTransform, chestSlot.SlotRectTransform);
        view.AddChestButtonListener();
    }

    // get called when OpenChest button is clicked
    public void ChestButtonClickedOn()
    {
        if (!view.gameObject.activeSelf) return;

        currentState.OnChestClicked();
    }

    // get called when Start Unlocking button is clicked
    public void StartUnlocking()
    {
        if (!view.gameObject.activeSelf) return;

        currentState.OnExit(); 
        currentState = chestUnlocking;
        currentState.OnEnter();
    }

    // get called when Unlock Now button is clicked
    public void UnlockChest()
    {
        if (!view.gameObject.activeSelf) return;

        currentState.OnExit();
        currentState = chestUnlocked;
        currentState.OnEnter();

        // add event that unlock now button is clicked and decrese the gems
        SlotService.Instance.StartNextChestUnlocking();
    }

    public void RemoveChestFromSlot()
    {
        if (view != null)
        {
            chestSlot.IsEmpty = true;
            view.RemoveChest();
            view = null;
        }
    }

    public void EnableChest()
    {
        view.EnableChest();
    }

    public void DisableChest()
    {
        view.DisableChest();
    }

    public void UpdateChestImage()
    {
        if (CurrentState == EChestState.LOCKED)
            view.UpdateChestImage(model.ChestClosedImage);
        else if(CurrentState == EChestState.UNLOCKED)
            view.UpdateChestImage(model.ChestOpenImage);
    }

    public void UpdateCurrentStateText(string currentStateName)
    {
        view.UpdateChestStateText(currentStateName);
    }

    public void UpdateTimeLeftUntilUnlockText(string timeLeftUntilUnlock)
    {
        view.UpdateTimeLeftUntilUnlockText(timeLeftUntilUnlock);
    }

    public int GenerateRewardCoins() // Generate from currency service
    {
        int coinsMin = model.CoinsMin;
        int coinsMax = model.CoinsMax;

        return UnityEngine.Random.Range(coinsMin, coinsMax + 1);
    }

    public int GenerateRewardGems() // Generate from currency service
    {
        int gemsMin = model.GemsMin;
        int gemsMax = model.GemsMax;

        return UnityEngine.Random.Range(gemsMin, gemsMax + 1);
    }
}