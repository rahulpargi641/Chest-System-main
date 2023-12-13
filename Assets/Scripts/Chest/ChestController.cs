using System;
using UnityEngine;

public class ChestController
{
    public ChestModel Model { get; private set; }
    public ChestView View { get; private set; }
    public EChestState CurrentState => currentState.GetChestState();

    private ChestLockedState chestLocked;
    private ChestUnlockingState chestUnlocking;
    private ChestUnlockedState chestUnlocked;
    private IChestState currentState;

    private ChestSlot chestSlot;

    public ChestController(ChestModel model, ChestView view)
    {
        Model = model;
        View = view;
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
        currentState.OnStateEnter();
    }

    public void SetupChest(ChestSlot chestSlot, Transform chestParentTransform)
    {
        this.chestSlot = chestSlot;

        View.SetRectTransform(chestParentTransform, chestSlot.SlotRectTransform);
        View.AddChestButtonListener();
    }

    // get called when OpenChest button is clicked
    public void ChestButtonClickedOn()
    {
        if (!View.gameObject.activeSelf) return;

        currentState.ChestButtonClickedOn();
    }

    // get called when Start Unlocking button is clicked
    public void StartUnlocking()
    {
        if (!View.gameObject.activeSelf) return;

        currentState.OnStateExit(); 
        currentState = chestUnlocking;
        currentState.OnStateEnter();
    }

    // get called when Unlock Now button is clicked
    public void UnlockNow()
    {
        if (!View.gameObject.activeSelf) return;

        currentState.OnStateExit();
        currentState = chestUnlocked;
        currentState.OnStateEnter();

        SlotService.Instance.StartNextChestUnlocking();
    }

    public void RemoveChestFromSlot()
    {
        if (View != null)
        {
            chestSlot.IsEmpty = true;
            View.RemoveChest();
            View = null;
        }
    }

    public void EnableChest()
    {
        View.EnableChest();
    }

    public void DisableChest()
    {
        View.DisableChest();
    }
}