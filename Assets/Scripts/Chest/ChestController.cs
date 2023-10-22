using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController
{
    public ChestModel ChestModel { get; private set; }
    public ChestView ChestView { get; private set; }

    private IChestState currentState;
    private ChestLockedState chestLocked;
    private ChestUnlockingState chestUnlocking;
    private ChestUnlockedState chestUnlocked;

    public float TimeSecondsPerGem { get { return 600f; } private set { } } //10 minutes

    public ChestController(ChestModel chestModel, ChestView chestPrefab)
    {
        this.ChestModel = chestModel;
        this.ChestView = GameObject.Instantiate<ChestView>(chestPrefab);

        ChestModel.SetController(this);
        ChestView.SetController(this);

        chestLocked = new ChestLockedState(this);
        chestUnlocking = new ChestUnlockingState(this);
        chestUnlocked = new ChestUnlockedState(this);

        currentState = chestLocked;
        currentState.OnStateEnable();
    }
    public void ChestButtonAction()
    {
        currentState.ChestButtonAction();
    }
    public void UnlockNow()
    {
        PlayerService.Instance.DecrementGems(currentState.GetRequiredGemsToUnlock());

        currentState.OnStateDisable();
        currentState = chestUnlocked;
        currentState.OnStateEnable();
    }
    public void StartUnlocking()
    {
        currentState.OnStateDisable();
        currentState = chestUnlocking;
        currentState.OnStateEnable();
    }
}